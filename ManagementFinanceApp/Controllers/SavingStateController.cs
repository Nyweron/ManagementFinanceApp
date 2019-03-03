using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.SavingState;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SavingStateController : ControllerBase
  {
    private ISavingStateRepository _savingStateRepository;
    private IMapper _mapper;
    public SavingStateController(ISavingStateRepository savingStateRepository,
      IMapper mapper
    )
    {
      _savingStateRepository = savingStateRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var savingStateEntities = await _savingStateRepository.GetAllAsync();
      return Ok(savingStateEntities);
    }

    [HttpGet("{savingStateId}")]
    public async Task<IActionResult> Get(int savingStateId)
    {
      try
      {
        var savingStateEntities = await _savingStateRepository.GetAsync(savingStateId);
        return Ok(savingStateEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {savingStateId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.SavingState savingState)
    {
      if (savingState == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto savingState).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var savingStateEntity = _mapper.Map<Entities.SavingState>(savingState);
      await _savingStateRepository.AddAsync(savingStateEntity);

      if (!await _savingStateRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      var savingState = await _savingStateRepository.GetAsync(id);

      if (!await _savingStateRepository.RemoveAsync(savingState))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
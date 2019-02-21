using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Saving;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SavingController : ControllerBase
  {
    private ISavingRepository _savingRepository;
    private IMapper _mapper;
    public SavingController(ISavingRepository savingRepository,
      IMapper mapper
    )
    {
      _savingRepository = savingRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var savingEntities = await _savingRepository.GetAllAsync();
      return Ok(savingEntities);
    }

    [HttpGet("{savingId}")]
    public async Task<IActionResult> Get(int savingId)
    {
      try
      {
        var savingEntities = await _savingRepository.GetAsync(savingId);
        return Ok(savingEntities);
      }
      catch (Exception ex)
      {
        // _logger.LogCritical($"Exception {savingId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Saving saving)
    {
      if (saving == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto saving).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var savingEntity = _mapper.Map<Entities.Saving>(saving);
      await _savingRepository.AddAsync(savingEntity);

      if (!await _savingRepository.SaveAsync())
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

      var saving = await _savingRepository.GetAsync(id);

      if (!await _savingRepository.RemoveAsync(saving))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
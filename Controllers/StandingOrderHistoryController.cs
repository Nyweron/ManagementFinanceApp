using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.StandingOrderHistory;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StandingOrderHistoryController : ControllerBase
  {
    private IStandingOrderHistoryRepository _standingOrderHistoryRepository;
    private IMapper _mapper;
    public StandingOrderHistoryController(IStandingOrderHistoryRepository standingOrderHistoryRepository,
      IMapper mapper
    )
    {
      _standingOrderHistoryRepository = standingOrderHistoryRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var standingOrderHistoryEntities = await _standingOrderHistoryRepository.GetAllAsync();
      return Ok(standingOrderHistoryEntities);
    }

    [HttpGet("{standingOrderHistoryId}")]
    public async Task<IActionResult> Get(int standingOrderHistoryId)
    {
      try
      {
        var standingOrderHistoryEntities = await _standingOrderHistoryRepository.GetAsync(standingOrderHistoryId);
        return Ok(standingOrderHistoryEntities);
      }
      catch (Exception ex)
      {
        // _logger.LogCritical($"Exception {standingOrderHistoryId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.StandingOrderHistory standingOrderHistory)
    {
      if (standingOrderHistory == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto standingOrderHistory).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var standingOrderHistoryEntity = _mapper.Map<Entities.StandingOrderHistory>(standingOrderHistory);
      await _standingOrderHistoryRepository.AddAsync(standingOrderHistoryEntity);

      if (!await _standingOrderHistoryRepository.SaveAsync())
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
      var standingOrderHistory = await _standingOrderHistoryRepository.GetAsync(id);

      if (!await _standingOrderHistoryRepository.RemoveAsync(standingOrderHistory))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
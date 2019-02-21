using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.StandingOrder;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StandingOrderController : ControllerBase
  {
    private IStandingOrderRepository _standingOrderRepository;
    private IMapper _mapper;
    public StandingOrderController(IStandingOrderRepository standingOrderRepository,
      IMapper mapper
    )
    {
      _standingOrderRepository = standingOrderRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var standingOrderEntities = await _standingOrderRepository.GetAllAsync();
      return Ok(standingOrderEntities);
    }

    [HttpGet("{standingOrderId}")]
    public async Task<IActionResult> Get(int standingOrderId)
    {
      try
      {
        var standingOrderEntities = await _standingOrderRepository.GetAsync(standingOrderId);
        return Ok(standingOrderEntities);
      }
      catch (Exception ex)
      {
        // _logger.LogCritical($"Exception {standingOrderId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.StandingOrder standingOrder)
    {
      if (standingOrder == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto standingOrder).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var standingOrderEntity = _mapper.Map<Entities.StandingOrder>(standingOrder);
      await _standingOrderRepository.AddAsync(standingOrderEntity);

      if (!await _standingOrderRepository.SaveAsync())
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
      var standingOrder = await _standingOrderRepository.GetAsync(id);

      if (!await _standingOrderRepository.RemoveAsync(standingOrder))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Plan;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  public class PlanController
  {
    private IPlanRepository _planRepository;
    private IMapper _mapper;
    public PlanController(IPlanRepository planRepository,
      IMapper mapper
    )
    {
      _planRepository = planRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var planEntities = await _planRepository.GetAllAsync();
      return Ok(planEntities);
    }

    [HttpGet("{planId}")]
    public async Task<IActionResult> Get(int planId)
    {
      try
      {
        var planEntities = _planRepository.GetAsync(planId);
        return Ok(planEntities);
      }
      catch (Exception ex)
      {
        // _logger.LogCritical($"Exception {planId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Plan plan)
    {
      if (plan == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto plan).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var planEntity = _mapper.Map<Entities.Plan>(plan);
      await _planRepository.AddAsync(planEntity);

      if (!await _planRepository.SaveAsync())
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

      var plan = await _planRepository.GetAsync(id);

      if (!await _planRepository.RemoveAsync(plan))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
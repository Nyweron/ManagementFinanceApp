using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Restriction;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RestrictionController : ControllerBase
  {
    private IRestrictionRepository _restrictionRepository;
    private IMapper _mapper;
    public RestrictionController(IRestrictionRepository restrictionRepository,
      IMapper mapper
    )
    {
      _restrictionRepository = restrictionRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var restrictionEntities = await _restrictionRepository.GetAllAsync();
      return Ok(restrictionEntities);
    }

    [HttpGet("{restrictionId}")]
    public async Task<IActionResult> Get(int restrictionId)
    {
      try
      {
        var restrictionEntities = await _restrictionRepository.GetAsync(restrictionId);
        return Ok(restrictionEntities);
      }
      catch (Exception ex)
      {
        // _logger.LogCritical($"Exception {restrictionId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Restriction restriction)
    {
      if (restriction == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto restriction).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var restrictionEntity = _mapper.Map<Entities.Restriction>(restriction);
      await _restrictionRepository.AddAsync(restrictionEntity);

      if (!await _restrictionRepository.SaveAsync())
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

      var restriction = await _restrictionRepository.GetAsync(id);

      if (!await _restrictionRepository.RemoveAsync(restriction))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Frequency;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class FrequencyController : Controller
  {
    private IFrequencyRepository _frequencyRepository;
    private IMapper _mapper;
    public FrequencyController(IFrequencyRepository frequencyRepository,
      IMapper mapper
    )
    {
      _frequencyRepository = frequencyRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var frequencyEntities = await _frequencyRepository.GetAllAsync();
      return Ok(frequencyEntities);
    }

    [HttpGet("{frequencyId}")]
    public async Task<IActionResult> Get(int frequencyId)
    {
      try
      {
        var frequencyEntities = await _frequencyRepository.GetAsync(frequencyId);
        return Ok(frequencyEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {frequencyId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.Frequency> frequency)
    {
      if (!frequency.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto frequency).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var frequencyEntity = _mapper.Map<List<Entities.Frequency>>(frequency);
      await _frequencyRepository.AddRangeAsync(frequencyEntity);

      if (!await _frequencyRepository.SaveAsync())
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

      var frequency = await _frequencyRepository.GetAsync(id);

      if (!await _frequencyRepository.RemoveAsync(frequency))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Income;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class IncomeController : ControllerBase
  {
    private IIncomeRepository _incomeRepository;
    private IMapper _mapper;
    public IncomeController(IIncomeRepository incomeRepository,
      IMapper mapper
    )
    {
      _incomeRepository = incomeRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var incomeEntities = await _incomeRepository.GetAllAsync();
      return Ok(incomeEntities);
    }

    [HttpGet("{incomeId}")]
    public async Task<IActionResult> Get(int incomeId)
    {
      try
      {
        var incomeEntities = await _incomeRepository.GetAsync(incomeId);
        return Ok(incomeEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {incomeId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Income income)
    {
      if (income == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto income).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var incomeEntity = _mapper.Map<Entities.Income>(income);
      await _incomeRepository.AddAsync(incomeEntity);

      if (!await _incomeRepository.SaveAsync())
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

      var income = await _incomeRepository.GetAsync(id);

      if (!await _incomeRepository.RemoveAsync(income))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
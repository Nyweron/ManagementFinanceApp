using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.InvestmentSchedule;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class InvestmentScheduleController : Controller
  {
    private IInvestmentScheduleRepository _investmentScheduleRepository;
    private IMapper _mapper;
    public InvestmentScheduleController(IInvestmentScheduleRepository investmentScheduleRepository,
      IMapper mapper
    )
    {
      _investmentScheduleRepository = investmentScheduleRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var investmentScheduleEntities = await _investmentScheduleRepository.GetAllAsync();
      return Ok(investmentScheduleEntities);
    }

    [HttpGet("{investmentScheduleId}")]
    public async Task<IActionResult> Get(int investmentScheduleId)
    {
      try
      {
        var investmentScheduleEntities = await _investmentScheduleRepository.GetAsync(investmentScheduleId);
        return Ok(investmentScheduleEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {investmentScheduleId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.InvestmentSchedule investmentSchedule)
    {
      if (investmentSchedule == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto investmentSchedule).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var investmentScheduleEntity = _mapper.Map<Entities.InvestmentSchedule>(investmentSchedule);
      await _investmentScheduleRepository.AddAsync(investmentScheduleEntity);

      if (!await _investmentScheduleRepository.SaveAsync())
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

      var investmentSchedule = await _investmentScheduleRepository.GetAsync(id);

      if (!await _investmentScheduleRepository.RemoveAsync(investmentSchedule))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
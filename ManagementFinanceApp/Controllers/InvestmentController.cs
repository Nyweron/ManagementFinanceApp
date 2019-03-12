using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Investment;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class InvestmentController : ControllerBase
  {
    private IInvestmentRepository _investmentRepository;
    private IMapper _mapper;
    public InvestmentController(IInvestmentRepository investmentRepository,
      IMapper mapper
    )
    {
      _investmentRepository = investmentRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var investmentEntities = await _investmentRepository.GetAllAsync();
      return Ok(investmentEntities);
    }

    [HttpGet("{investmentId}")]
    public async Task<IActionResult> Get(int investmentId)
    {
      try
      {
        var investmentEntities = await _investmentRepository.GetAsync(investmentId);
        return Ok(investmentEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {investmentId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Investment investment)
    {
      if (investment == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto investment).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var investmentEntity = _mapper.Map<Entities.Investment>(investment);
      await _investmentRepository.AddAsync(investmentEntity);

      if (!await _investmentRepository.SaveAsync())
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

      var investment = await _investmentRepository.GetAsync(id);

      if (!await _investmentRepository.RemoveAsync(investment))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
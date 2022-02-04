using System;
using System.Threading.Tasks;
using ManagementFinanceApp.Service.Income;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class IncomeController : ControllerBase
  {
    private IIncomeService _incomeService;
    private ILogger _logger;

    public IncomeController(IIncomeService incomeService, ILogger logger)
    {
      _incomeService = incomeService;
      _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var incomeEntities = await _incomeService.GetAllAdaptAsync();
      return Ok(incomeEntities);
    }

    [HttpGet("{incomeId}")]
    public async Task<IActionResult> Get(int incomeId)
    {
      try
      {
        var incomeEntities = await _incomeService.GetAsync(incomeId);
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
      //TODO: Check problems with date...
      if (income == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto income).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _incomeService.AddIncome(income);

      if (isCreated)
      {
        return Created("", null);
      }
      else
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      var income = await _incomeService.GetAsync(id);

      if (!await _incomeService.RemoveAsync(income))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Models.Income incomeRequest)
    {
      try
      {
        if (incomeRequest == null)
        {
          return BadRequest("Object cannot be null");
        }

        // Update entity in repository
        var isUpdated = await _incomeService.EditIncome(incomeRequest, id);
        if (isUpdated)
        {
          return NoContent();
        }
        else
        {
          _logger.LogError($"Edit income a problem happend. Error in updateExpense. When accessing to ExpenseController/Edit");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        // Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutStockItemAsync), ex);
      }

      return NoContent();
    }
  }
}
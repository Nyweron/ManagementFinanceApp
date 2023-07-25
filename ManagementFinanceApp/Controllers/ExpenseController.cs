using System;
using System.Threading.Tasks;
using ManagementFinanceApp.Service.Expense;
using ManagementFinanceApp.Service.UserContextService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ManagementFinanceApp.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ExpenseController : ControllerBase
  {
    private IExpenseService _expenseService;
    private ILogger _logger;
    private readonly IUserContextService _userContextService;

    public ExpenseController(IExpenseService expenseService, ILogger logger, IUserContextService userContextService)
    {
      _expenseService = expenseService;
      _logger = logger;
      _userContextService = userContextService;
    }

    [HttpGet]
    // [Authorize(Policy = "Atleast20")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {


      var bar = _userContextService.GetUserId;
      var foo = _userContextService.User;
      var expenseModels = await _expenseService.GetAllAdaptAsync();
      return Ok(expenseModels);
    }

    [HttpGet("{expenseId}")]
    public async Task<IActionResult> Get(int expenseId)
    {
      var expenseEntities = await _expenseService.GetAsync(expenseId);
      return Ok(expenseEntities);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Post([FromBody] Models.Expense expense)
    {
      //TODO: Check problems with date...
      if (expense == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto expense).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _expenseService.AddExpense(expense);

      if (isCreated)
      {
        return Created("", null);
      }
      else
      {

        dynamic city = new System.Dynamic.ExpandoObject();
        city.Text = "Problem with backend api";
        city.TextTwo = "Bug sTatsu 0992";

        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, city);
      }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "HasNick")]
    public async Task<IActionResult> Delete(int id)
    {

      var expense = await _expenseService.GetAsync(id);

      if (expense == null)
      {
        return NotFound();
      }

      if (!await _expenseService.RemoveAsync(expense))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Models.Expense expenseRequest)
    {
      try
      {
        if (expenseRequest == null)
        {
          return BadRequest("Object cannot be null");
        }


        // Update entity in repository
        var isUpdated = await _expenseService.EditExpense(expenseRequest, id);
        if (isUpdated)
        {
          return NoContent();
        }
        else
        {
          _logger.LogError($"Edit expense a problem happend. Error in updateExpense. When accessing to ExpenseController/Edit");
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
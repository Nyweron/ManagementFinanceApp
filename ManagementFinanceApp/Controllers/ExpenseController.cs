using System;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class ExpenseController : ControllerBase
  {
    private IExpenseRepository _expenseRepository;
    private IMapper _mapper;
    public ExpenseController(IExpenseRepository expenseRepository,
      IMapper mapper
    )
    {
      _expenseRepository = expenseRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var expenseEntities = await _expenseRepository.GetAllAsync();
      return Ok(expenseEntities);
    }

    [HttpGet("{expenseId}")]
    public async Task<IActionResult> Get(int expenseId)
    {
      try
      {
        var expenseEntities = await _expenseRepository.GetAsync(expenseId);
        return Ok(expenseEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {expenseId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Expense expense)
    {
      if (expense == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto expense).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var expenseEntity = _mapper.Map<Entities.Expense>(expense);
      await _expenseRepository.AddAsync(expenseEntity);

      if (!await _expenseRepository.SaveAsync())
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

      var expense = await _expenseRepository.GetAsync(id);

      if (!await _expenseRepository.RemoveAsync(expense))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategoryIncome;
using ManagementFinanceApp.Service.CategoryIncome;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryIncomeController : ControllerBase
  {
    private ICategoryIncomeService _categoryIncomeService;

    public CategoryIncomeController(ICategoryIncomeService categoryIncomeService)
    {
      _categoryIncomeService = categoryIncomeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var categoryIncomeEntities = await _categoryIncomeService.GetAllAsync();
      return Ok(categoryIncomeEntities);
    }

    [HttpGet("{categoryIncomeId}")]
    public async Task<IActionResult> Get(int categoryIncomeId)
    {
      var categoryIncomeEntities = await _categoryIncomeService.GetAsync(categoryIncomeId);
      return Ok(categoryIncomeEntities);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.CategoryIncome> categoryIncome)
    {
      if (!categoryIncome.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categoryIncome).");
        return BadRequest();
      }

      if (!ModelState.IsValid) //przekazac do middelware obsluge zwracanaia wyjatkow itp...
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _categoryIncomeService.AddCategoryIncome(categoryIncome);

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
      var categoryIncome = await _categoryIncomeService.GetAsync(id);

      if (categoryIncome == null)
      {
        return NotFound();
      }

      if (!await _categoryIncomeService.RemoveAsync(categoryIncome))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Models.CategoryIncome categoryIncomeRequest)
    {
      try
      {
        if (categoryIncomeRequest == null)
        {
          return BadRequest("Object cannot be null");
        }

        // Update entity in repository
        var isUpdated = await _categoryIncomeService.EditCategoryIncome(categoryIncomeRequest, id);
        if (isUpdated)
        {
          return NoContent();
        }
        else
        {
          // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        //response.DidError = true;
        //response.ErrorMessage = "There was an internal error, please contact to technical support.";

        // Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutStockItemAsync), ex);
      }

      return NoContent();
    }
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Service.CategoryExpense;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryExpenseController : ControllerBase
  {
    private ICategoryExpenseService _categoryExpenseService;
    private ICategoryExpenseAdapter _categoryExpenseAdapter;

    //controller -> service-> repozytorium. w service ma byc lacznikiem, w service jest cala 'logika' Encje model itp...
    //controller nie powinnien miec dostepu do repozytorium! brak Encji(obiekt bazodanowy)
    //nie powinno na domenie pracować
    //Kontroller ma przyjmować dane a reszta ma sie dziac w service

    //https://en.wikipedia.org/wiki/GRASP_(object-oriented_design)

    public CategoryExpenseController(ICategoryExpenseService categoryExpenseService,
                                     ICategoryExpenseAdapter categoryExpenseAdapter)
    {
      _categoryExpenseService = categoryExpenseService;
      _categoryExpenseAdapter = categoryExpenseAdapter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var categoryExpenseEntities = await _categoryExpenseService.GetAllAsync();
      return Ok(categoryExpenseEntities);
    }


    [HttpGet("GetCategoryExpensesForSelect")]
    public async Task<IActionResult> GetCategoryExpensesForSelect()
    {
      var categoryExpenseViewList = await _categoryExpenseAdapter.GetCategoryExpenseList();
      return Ok(categoryExpenseViewList);
    }


    [HttpGet("{categoryExpenseId}")]
    public async Task<IActionResult> Get(int categoryExpenseId)
    {
      //tak powinien wygladac kontroller(max dwie linijki) (wyjatki try catch powinny byc obslugiwane w middelwarre)
      var categoryExpenseEntities = await _categoryExpenseService.GetAsync(categoryExpenseId);
      return Ok(categoryExpenseEntities);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.CategoryExpense categoryExpense)
    {
      if (categoryExpense == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categoryExpense).");
        return BadRequest();
      }

      if (!ModelState.IsValid) //przekazac do middelware obsluge zwracanaia wyjatkow itp...
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _categoryExpenseService.AddCategoryExpense(categoryExpense);

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
      var categoryExpense = await _categoryExpenseService.GetAsync(id);

      if (categoryExpense == null)
      {
        return NotFound();
      }

      if (!await _categoryExpenseService.RemoveAsync(categoryExpense))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] Models.CategoryExpense categoryExpenseRequest)
    {
      try
      {
        if (categoryExpenseRequest == null)
        {
          return BadRequest("Object cannot be null");
        }

        // Update entity in repository
        var isUpdated = await _categoryExpenseService.EditCategoryExpense(categoryExpenseRequest, id);
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
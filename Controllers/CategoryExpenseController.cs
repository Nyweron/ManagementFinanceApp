using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategoryExpense;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryExpenseController : Controller
  {
    private ICategoryExpenseRepository _categoryExpenseRepository;
    private IMapper _mapper;
    public CategoryExpenseController(ICategoryExpenseRepository categoryExpenseRepository,
      IMapper mapper
    )
    {
      _categoryExpenseRepository = categoryExpenseRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var categoryExpenseEntities = await _categoryExpenseRepository.GetAllAsync();
      return Ok(categoryExpenseEntities);
    }

    [HttpGet("{categoryExpenseId}")]
    public async Task<IActionResult> Get(int categoryExpenseId)
    {
      try
      {
        var categoryExpenseEntities = await _categoryExpenseRepository.GetAsync(categoryExpenseId);
        return Ok(categoryExpenseEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {categoryExpenseId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.CategoryExpense> categoryExpense)
    {
      if (!categoryExpense.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categoryExpense).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var categoryExpenseEntity = _mapper.Map<List<Entities.CategoryExpense>>(categoryExpense);
      await _categoryExpenseRepository.AddRangeAsync(categoryExpenseEntity);

      try
      {
        if (!await _categoryExpenseRepository.SaveAsync())
        {
          // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      var categoryExpense = await _categoryExpenseRepository.GetAsync(id);

      if (!await _categoryExpenseRepository.RemoveAsync(categoryExpense))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
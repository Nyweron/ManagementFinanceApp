using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategoryIncome;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryIncomeController : Controller
  {
    private ICategoryIncomeRepository _categoryIncomeRepository;
    private IMapper _mapper;
    public CategoryIncomeController(ICategoryIncomeRepository categoryIncomeRepository,
      IMapper mapper
    )
    {
      _categoryIncomeRepository = categoryIncomeRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var categoryIncomeEntities = await _categoryIncomeRepository.GetAllAsync();
      return Ok(categoryIncomeEntities);
    }

    [HttpGet("{categoryIncomeId}")]
    public async Task<IActionResult> Get(int categoryIncomeId)
    {
      try
      {
        var categoryIncomeEntities = await _categoryIncomeRepository.GetAsync(categoryIncomeId);
        return Ok(categoryIncomeEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {categoryIncomeId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.CategoryIncome> categoryIncome)
    {
      if (!categoryIncome.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categoryIncome).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {

        var categoryIncomeEntity = _mapper.Map<List<Entities.CategoryIncome>>(categoryIncome);
        await _categoryIncomeRepository.AddRangeAsync(categoryIncomeEntity);

        if (!await _categoryIncomeRepository.SaveAsync())
        {
          // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        Debug.WriteLine(ex);
        return BadRequest(ex.InnerException.Message);
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

      var categoryIncome = await _categoryIncomeRepository.GetAsync(id);

      if (!await _categoryIncomeRepository.RemoveAsync(categoryIncome))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
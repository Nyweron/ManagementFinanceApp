using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategorySaving;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategorySavingController : Controller
  {
    private ICategorySavingRepository _categorySavingRepository;
    private IMapper _mapper;
    public CategorySavingController(ICategorySavingRepository categorySavingRepository,
      IMapper mapper
    )
    {
      _categorySavingRepository = categorySavingRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var categorySavingEntities = await _categorySavingRepository.GetAllAsync();
      return Ok(categorySavingEntities);
    }

    [HttpGet("{categorySavingId}")]
    public async Task<IActionResult> Get(int categorySavingId)
    {
      try
      {
        var categorySavingEntities = await _categorySavingRepository.GetAsync(categorySavingId);
        return Ok(categorySavingEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {categorySavingId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.CategorySaving> categorySaving)
    {
      if (!categorySaving.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categorySaving).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var categorySavingEntity = _mapper.Map<List<Entities.CategorySaving>>(categorySaving);
      await _categorySavingRepository.AddRangeAsync(categorySavingEntity);

      if (!await _categorySavingRepository.SaveAsync())
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

      var categorySaving = await _categorySavingRepository.GetAsync(id);

      if (!await _categorySavingRepository.RemoveAsync(categorySaving))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }
  }
}
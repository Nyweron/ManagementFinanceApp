using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategoryGroup;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryGroupController : Controller
  {
    private ICategoryGroupRepository _categoryGroupRepository;
    private IMapper _mapper;
    public CategoryGroupController(ICategoryGroupRepository categoryGroupRepository,
      IMapper mapper
    )
    {
      _categoryGroupRepository = categoryGroupRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var getAll = await _categoryGroupRepository.GetAllAsync();
      return Ok(getAll);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<Models.CategoryGroup> categoryGroup)
    {
      if (!categoryGroup.Any())
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto categoryGroups).");
        return BadRequest();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var categorySavingEntity = _mapper.Map<List<Entities.CategoryGroup>>(categoryGroup);
      await _categoryGroupRepository.AddRangeAsync(categorySavingEntity);
      try
      {
        if (!await _categoryGroupRepository.SaveAsync())
        {
          // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
          return StatusCode(500, "A problem happend while handling your request.");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
        Debug.WriteLine("\n\tError Message", ex);
        return BadRequest(ex.InnerException.Message);
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

  }
}
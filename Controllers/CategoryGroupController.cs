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
  }
}
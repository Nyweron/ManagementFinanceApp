using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Income;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class IncomeController : Controller
  {
    private IIncomeRepository _incomeRepository;
    private IMapper _mapper;
    public IncomeController(IIncomeRepository incomeRepository,
      IMapper mapper
    )
    {
      _incomeRepository = incomeRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var getAll = await _incomeRepository.GetAllAsync();
      return Ok(getAll);
    }

  }
}
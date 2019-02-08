using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Expense;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ExpenseController : Controller
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
      var getAll = await _expenseRepository.GetAllAsync();
      return Ok(getAll);
    }

  }
}
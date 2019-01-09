using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly ManagementFinanceAppDbContext _context;
    private IUserRepository _userRepository;

    public UserController(ManagementFinanceAppDbContext context, IUserRepository userRepository)
    {
      _context = context;
      _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      var userEntities = _userRepository.GetAllAsync().Result;
      return Ok(userEntities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
      var todoItem = await _context.Users.FindAsync(id);

      if (todoItem == null)
      {
        return NotFound();
      }

      return todoItem;
    }

  }
}
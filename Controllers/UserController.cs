using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly ManagementFinanceAppDbContext _context;

    public UserController(ManagementFinanceAppDbContext context)
    {
      _context = context;

      if (_context.Users.Count() == 0)
      {
        // Create a new TodoItem if collection is empty,
        // which means you can't delete all TodoItems.
        _context.Users.Add(new User
        {
          FirstName = "FirstName 1",
            LastName = "LastName 1",
            Nick = "Nick 1",
            Email = "Email1",
            IsDelete = false
        });
        _context.SaveChanges();
      }
    }
    // GET: api/Todo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
      return await _context.Users.ToListAsync();
    }

    // GET: api/Todo/5
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
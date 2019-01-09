using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementFinanceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Repository.User
{
  public class UserRepository : IUserRepository
  {
    private readonly ManagementFinanceAppDbContext _context;
    public UserRepository(ManagementFinanceAppDbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<Entities.User>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }
  }
}
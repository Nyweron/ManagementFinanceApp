using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementFinanceApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Repository.User
{
  public class UserRepository : Repository<Entities.User>, IUserRepository
  {
    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public UserRepository(ManagementFinanceAppDbContext context) : base(context) { }

    public bool UserExists(int userId)
    {
      return ManagementFinanceAppDbContext.Users.Any(c => c.Id == userId);
    }
    public async Task<bool> UserExistsAsync(int userId)
    {
      //Is this good way?
      var userExist = await ManagementFinanceAppDbContext.Users.FindAsync(userId);
      return userExist != null ? true : false;
    }

    public bool EmailExists(string email)
    {
      return ManagementFinanceAppDbContext.Users.Any(c => c.Email == email);
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
      //Is this good way?
      return await ManagementFinanceAppDbContext.Users.AnyAsync(c => c.Email == email);
    }

    public void UpdateUser(Entities.User user)
    {
      ManagementFinanceAppDbContext.Update(user);
    }

    public async Task UpdateUserAsync(Entities.User user)
    {
      var itemToUpdate = await ManagementFinanceAppDbContext.Users.SingleOrDefaultAsync(r => r.Id == user.Id);
      if (itemToUpdate != null)
      {
        itemToUpdate.FirstName = user.FirstName;
        itemToUpdate.LastName = user.LastName;
        itemToUpdate.Nick = user.Nick;
        itemToUpdate.Address = user.Address;
        itemToUpdate.Phone = user.Phone;
        itemToUpdate.Email = user.Email;
      }
    }

    public bool Save()
    {
      return (ManagementFinanceAppDbContext.SaveChanges() >= 0);
    }
    public async Task<bool> SaveAsync()
    {
      var result = await ManagementFinanceAppDbContext.SaveChangesAsync();
      return (result >= 0);
    }

  }
}
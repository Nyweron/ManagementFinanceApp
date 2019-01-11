using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.User
{
  public interface IUserRepository : IRepository<Entities.User>
  {
    bool UserExists(int userId);
    Task<bool> UserExistsAsync(int userId);
    bool EmailExists(string email);
    Task<bool> EmailExistsAsync(string email);
    void UpdateUser(Entities.User user);
    Task UpdateUserAsync(Entities.User user);
    bool Save();
    Task<bool> SaveAsync();
  }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.User
{
  public interface IUserService
  {
    Task<bool> AddUser(Models.UserDto user);
    Task<bool> EditUser(Models.UserDto user, int userId);
    Task<IEnumerable<Entities.User>> GetAllAsync();
  }
}
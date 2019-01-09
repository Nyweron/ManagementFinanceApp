using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.User
{
  public interface IUserRepository
  {
    Task<IEnumerable<Entities.User>> GetAllAsync();
  }
}
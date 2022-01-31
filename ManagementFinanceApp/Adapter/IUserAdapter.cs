using ManagementFinanceApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface IUserAdapter
  {
    public Task<IEnumerable<ViewDataForSelect>> GetUserList();
  }
}

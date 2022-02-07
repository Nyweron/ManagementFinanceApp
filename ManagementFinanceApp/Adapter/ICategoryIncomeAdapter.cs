using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface ICategoryIncomeAdapter
  {
    public Task<IEnumerable<Models.ViewDataForSelect>> GetCategoryIncomeList();
  }
}

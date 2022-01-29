using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface ICategoryExpenseAdapter
  {
    public Task<IEnumerable<Models.CategoryExpenseViewForSelect>> GetCategoryExpenseList();
  }
}

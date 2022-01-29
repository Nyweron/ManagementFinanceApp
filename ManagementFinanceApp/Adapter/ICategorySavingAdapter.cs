using ManagementFinanceApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface ICategorySavingAdapter
  {
    public Task<IEnumerable<CategorySavingViewForSelect>> GetCategorySavingList();
  }
}

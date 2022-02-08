using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface ISavingAdapter
  { 
    public Task<IEnumerable<Models.List.SavingList>> AdaptSaving();
  }
}

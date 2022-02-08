using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface IIncomeAdapter
  {
    public Task<IEnumerable<Models.List.IncomeList>> AdaptIncome();
  }
}

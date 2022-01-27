using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public interface IExpenseAdapter
  {
    public Task<IEnumerable<Models.Expense>> AdaptExpense();
  }
}

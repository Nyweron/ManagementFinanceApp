using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.Expense
{
  public interface IExpenseService
  {
    Task<IEnumerable<Entities.Expense>> GetAllAsync();
    Task<Entities.Expense> GetAsync(int categoryExpenseId);
    Task<bool> AddExpense(Models.Expense expense);
    Task<bool> RemoveAsync(Entities.Expense expense);
    Task<bool> EditExpense(Models.Expense expense, int id);
    Task<IEnumerable<Models.List.ExpenseList>> GetAllAdaptAsync();
  }
}
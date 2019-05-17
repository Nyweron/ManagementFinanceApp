using System.Collections.Generic;
using System.Threading.Tasks;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;

namespace ManagementFinanceApp.Service.Expense
{
  public class ExpenseService : IExpenseService
  {
    public Task<bool> AddExpense(Models.Expense expense)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> EditExpense(Models.Expense expense, int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Entities.Expense>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task<Entities.Expense> GetAsync(int categoryExpenseId)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> RemoveAsync(Entities.Expense expense)
    {
      throw new System.NotImplementedException();
    }
  }
}
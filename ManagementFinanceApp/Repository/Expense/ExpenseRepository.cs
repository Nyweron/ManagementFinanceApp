using System;
using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.Expense
{
  public class ExpenseRepository : Repository<Entities.Expense>, IExpenseRepository
  {
    public ExpenseRepository(ManagementFinanceAppDbContext context) : base(context) { }

    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public async Task<bool> SaveAsync()
    {
      try
      {
        var result = await ManagementFinanceAppDbContext.SaveChangesAsync();
        return (result >= 0);
      }
      catch (Exception ex)
      {
        throw new Exception("Error message", ex);
      }
    }
  }
}
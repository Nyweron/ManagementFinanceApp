using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.Expense
{
  public class ExpenseRepository : Repository<Entities.Expense>, IExpenseRepository
  {
    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public ExpenseRepository(ManagementFinanceAppDbContext context) : base(context) { }
  }
}
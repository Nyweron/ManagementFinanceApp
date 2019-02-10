using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.Income
{
  public class IncomeRepository : Repository<Entities.Income>, IIncomeRepository
  {
    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public IncomeRepository(ManagementFinanceAppDbContext context) : base(context) { }
  }
}
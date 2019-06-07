using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.StandingOrderHistory
{
  public class StandingOrderHistoryRepository : Repository<Entities.StandingOrderHistory>, IStandingOrderHistoryRepository
  {
    public StandingOrderHistoryRepository(ManagementFinanceAppDbContext context) : base(context) { }

    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public async Task<bool> SaveAsync()
    {
      var result = await ManagementFinanceAppDbContext.SaveChangesAsync();
      return (result >= 0);
    }
  }
}
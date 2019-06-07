using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.SavingState
{
  public class SavingStateRepository : Repository<Entities.SavingState>, ISavingStateRepository
  {
    public SavingStateRepository(ManagementFinanceAppDbContext context) : base(context) { }

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
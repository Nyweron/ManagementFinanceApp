using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.Frequency
{
  public class FrequencyRepository : Repository<Entities.Frequency>, IFrequencyRepository
  {
    public FrequencyRepository(ManagementFinanceAppDbContext context) : base(context) { }

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
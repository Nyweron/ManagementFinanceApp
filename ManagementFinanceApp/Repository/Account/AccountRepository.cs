using ManagementFinanceApp.Data;
using System;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Account
{
  public class AccountRepository : Repository<Entities.User>, IAccountRepository
  {
    public AccountRepository(ManagementFinanceAppDbContext context) : base(context) { }

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

    public void SaveChanves()
    {
      try
      {
        ManagementFinanceAppDbContext.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception("Error message", ex);
      }
    }
  }
}

using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Account
{
  public interface IAccountRepository : IRepository<Entities.User>
  {
    Task<bool> SaveAsync();
    void SaveChanves();
  }
}

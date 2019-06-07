using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.StandingOrderHistory
{
  public interface IStandingOrderHistoryRepository : IRepository<Entities.StandingOrderHistory>
  {
    Task<bool> SaveAsync();
  }
}
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.StandingOrder
{
  public interface IStandingOrderRepository : IRepository<Entities.StandingOrder>
  {
    Task<bool> SaveAsync();
  }
}
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Plan
{
  public interface IPlanRepository : IRepository<Entities.Plan>
  {
    Task<bool> SaveAsync();
  }
}
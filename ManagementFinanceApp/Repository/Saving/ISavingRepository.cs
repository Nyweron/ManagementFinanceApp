using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Saving
{
  public interface ISavingRepository : IRepository<Entities.Saving>
  {
    Task<bool> SaveAsync();
  }
}
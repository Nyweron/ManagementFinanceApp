using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.SavingState
{
  public interface ISavingStateRepository : IRepository<Entities.SavingState>
  {
    Task<bool> SaveAsync();
  }
}
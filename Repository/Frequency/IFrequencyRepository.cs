using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Frequency
{
  public interface IFrequencyRepository : IRepository<Entities.Frequency>
  {
    Task<bool> SaveAsync();
  }
}
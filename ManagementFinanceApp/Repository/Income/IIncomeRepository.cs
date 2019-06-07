using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Income
{
  public interface IIncomeRepository : IRepository<Entities.Income>
  {
    Task<bool> SaveAsync();
  }
}
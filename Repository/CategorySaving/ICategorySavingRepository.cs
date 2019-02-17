using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.CategorySaving
{
  public interface ICategorySavingRepository : IRepository<Entities.CategorySaving>
  {
    Task<bool> SaveAsync();
  }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.CategorySaving
{
  public interface ICategorySavingService
  {
    Task<IEnumerable<Entities.CategorySaving>> GetAllAsync();
    Task<Entities.CategorySaving> GetAsync(int categorySavingId);
    Task<bool> AddCategorySaving(Models.CategorySaving categorySaving);
    Task<bool> RemoveAsync(Entities.CategorySaving categorySaving);
    Task<bool> EditCategorySaving(Models.CategorySaving categorySaving, int id);
  }
}

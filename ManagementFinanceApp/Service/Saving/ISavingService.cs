using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.Saving
{
  public interface ISavingService
  {
    Task<IEnumerable<Entities.Saving>> GetAllAsync();
    Task<Entities.Saving> GetAsync(int categorySavingId);
    Task<bool> AddSaving(Models.Saving saving);
    Task<bool> RemoveAsync(Entities.Saving saving);
    Task<bool> EditSaving(Models.Saving saving, int id);
    Task<IEnumerable<Models.List.SavingList>> GetAllAdaptAsync();
  }
}

using ManagementFinanceApp.Repository.CategorySaving;
using ManagementFinanceApp.Repository.Saving;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class SavingAdapter : ISavingAdapter
  {
    private ISavingRepository _savingRepository;
    private ICategorySavingRepository _categorySavingRepository;

    public SavingAdapter(ISavingRepository savingRepository,
                         ICategorySavingRepository categorySavingRepository)
    {
      _savingRepository = savingRepository;
      _categorySavingRepository = categorySavingRepository;
    }

    public async Task<IEnumerable<Models.List.SavingList>> AdaptSaving()
    {
      var savingsModelList = new List<Models.List.SavingList>();

      var savings = await _savingRepository.GetAllAsync();

      foreach (var saving in savings)
      {
        var categorySaving = await _categorySavingRepository.GetAsync(saving.CategorySavingId);
        if (categorySaving == null)
        {
          continue;
        }

        savingsModelList.Add(new Models.List.SavingList
        {
          Id = saving.Id,
          CategorySavingId = saving.CategorySavingId,
          CategorySavingDescription = categorySaving.Description,
          Comment = saving.Comment,
          Date = saving.Date,
          HowMuch = saving.HowMuch,
          SavingType = saving.SavingType,
        });

      }

      return savingsModelList;
    }
  }
}

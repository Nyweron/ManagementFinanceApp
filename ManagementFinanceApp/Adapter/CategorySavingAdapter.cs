using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.CategorySaving;
using ManagementFinanceApp.Service.CategorySaving;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class CategorySavingAdapter : ICategorySavingAdapter
  {

    private readonly ICategorySavingService _categorySavingService;

    public CategorySavingAdapter(ICategorySavingService categorySavingService)
    {
      _categorySavingService = categorySavingService;
    }

    public async Task<IEnumerable<CategorySavingViewForSelect>> GetCategorySavingList()
    {
      var categorySavings = await _categorySavingService.GetAllAsync();
      var categorySavingViewForSelect = new List<CategorySavingViewForSelect>();

      foreach (var categorySaving in categorySavings)
      {
        if (categorySaving == null)
        {
          continue;
        }

        categorySavingViewForSelect.Add(new CategorySavingViewForSelect
        {
          text = categorySaving.Description,
          value = categorySaving.Id.ToString(),
        });
      }


      return categorySavingViewForSelect;
    }

  }
}

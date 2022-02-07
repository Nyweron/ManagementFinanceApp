using ManagementFinanceApp.Models;
using ManagementFinanceApp.Service.CategoryIncome;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class CategoryIncomeAdapter : ICategoryIncomeAdapter
  {
    private readonly ICategoryIncomeService _categoryIncomeService;

    public CategoryIncomeAdapter(ICategoryIncomeService categoryIncomeService)
    {
      _categoryIncomeService = categoryIncomeService;
    }

    public async Task<IEnumerable<ViewDataForSelect>> GetCategoryIncomeList()
    {
      var categoryIncomes = await _categoryIncomeService.GetAllAsync();
      var categoryIncomeViewForSelect = new List<ViewDataForSelect>();

      foreach (var categoryIncome in categoryIncomes)
      {
        if (categoryIncome == null)
        {
          continue;
        }

        categoryIncomeViewForSelect.Add(new ViewDataForSelect
        {
          text = categoryIncome.Description,
          value = categoryIncome.Id.ToString(),
        });
      }

      return categoryIncomeViewForSelect;
    }
  }
}

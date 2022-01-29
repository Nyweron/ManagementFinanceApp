using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Service.CategoryExpense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class CategoryExpenseAdapter : ICategoryExpenseAdapter
  {
    private readonly ICategoryExpenseService _categoryExpenseService;
    private readonly ICategoryExpenseRepository _categoryExpenseRepository;

    public CategoryExpenseAdapter(ICategoryExpenseService categoryExpenseService,
                                  ICategoryExpenseRepository categoryExpenseRepository)
    {
      _categoryExpenseService = categoryExpenseService;
      _categoryExpenseRepository = categoryExpenseRepository;
    }

    public async Task<IEnumerable<CategoryExpenseViewForSelect>> GetCategoryExpenseList()
    {
      var categoryExpenses = await _categoryExpenseService.GetAllAsync();
      var categoryExpenseViewForSelect = new List<CategoryExpenseViewForSelect>();

      foreach (var categoryExpense in categoryExpenses)
      {
        if(categoryExpense == null)
        {
          continue;
        }

        categoryExpenseViewForSelect.Add(new CategoryExpenseViewForSelect
        {
          text = categoryExpense.Description,
          value = categoryExpense.Id.ToString(),
        });
      }


      return categoryExpenseViewForSelect;
    }
  }
}

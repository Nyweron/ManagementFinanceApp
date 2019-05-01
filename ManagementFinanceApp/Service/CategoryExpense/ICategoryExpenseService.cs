using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.CategoryExpense
{
  public interface ICategoryExpenseService
  {
    Task<bool> AddCategoryExpense(List<Models.CategoryExpense> categoryExpense);
    Task<bool> EditCategoryExpense(Models.CategoryExpense categoryExpense, int id);
  }
}
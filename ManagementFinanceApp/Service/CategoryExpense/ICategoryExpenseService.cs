using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.CategoryExpense
{
  public interface ICategoryExpenseService
  {
    Task<IEnumerable<Entities.CategoryExpense>> GetAllAsync();
    Task<Entities.CategoryExpense> GetAsync(int categoryExpenseId);
    Task<bool> AddCategoryExpense(List<Models.CategoryExpense> categoryExpense);
    Task<bool> RemoveAsync(Entities.CategoryExpense categoryExpense);
    Task<bool> EditCategoryExpense(Models.CategoryExpense categoryExpense, int id);
  }
}
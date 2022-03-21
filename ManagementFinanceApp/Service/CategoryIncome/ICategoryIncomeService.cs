using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.CategoryIncome
{
  public interface ICategoryIncomeService
  {
    Task<IEnumerable<Entities.CategoryIncome>> GetAllAsync();
    Task<Entities.CategoryIncome> GetAsync(int categoryExpenseId);
    Task<bool> AddCategoryIncome(Models.CategoryIncome categoryExpense);
    Task<bool> RemoveAsync(Entities.CategoryIncome categoryExpense);
    Task<bool> EditCategoryIncome(Models.CategoryIncome categoryExpense, int id);
  }
}
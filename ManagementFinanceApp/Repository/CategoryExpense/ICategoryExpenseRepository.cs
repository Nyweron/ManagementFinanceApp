using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.CategoryExpense
{
  public interface ICategoryExpenseRepository : IRepository<Entities.CategoryExpense>
  {
    Task<bool> SaveAsync();

  }
}
using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.CategoryExpense
{
  public class CategoryExpenseRepository : Repository<Entities.CategoryExpense>, ICategoryExpenseRepository
  {
    public CategoryExpenseRepository(ManagementFinanceAppDbContext context) : base(context) { }

    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }
    public async Task<bool> SaveAsync()
    {
      var result = await ManagementFinanceAppDbContext.SaveChangesAsync();
      return (result >= 0);
    }
  }
}
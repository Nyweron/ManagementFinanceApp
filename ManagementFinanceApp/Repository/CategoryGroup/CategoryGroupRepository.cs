using System.Threading.Tasks;
using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.CategoryGroup
{
  public class CategoryGroupRepository : Repository<Entities.CategoryGroup>, ICategoryGroupRepository
  {
    public CategoryGroupRepository(ManagementFinanceAppDbContext context) : base(context) { }

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
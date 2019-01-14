using ManagementFinanceApp.Data;

namespace ManagementFinanceApp.Repository.CategoryGroup
{
  public class CategoryGroupRepository : Repository<Entities.CategoryGroup>, ICategoryGroupRepository
  {
    public CategoryGroupRepository(ManagementFinanceAppDbContext context) : base(context)
    { }
  }
}
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.CategoryGroup
{
  public interface ICategoryGroupRepository : IRepository<Entities.CategoryGroup>
  {
    Task<bool> SaveAsync();
  }
}
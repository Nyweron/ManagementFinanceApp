using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.CategoryIncome
{
  public interface ICategoryIncomeRepository : IRepository<Entities.CategoryIncome>
  {
    Task<bool> SaveAsync();
  }
}
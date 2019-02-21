using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Restriction
{
  public interface IRestrictionRepository : IRepository<Entities.Restriction>
  {
    Task<bool> SaveAsync();
  }
}
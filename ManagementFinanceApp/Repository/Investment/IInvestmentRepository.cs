using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Investment
{
  public interface IInvestmentRepository : IRepository<Entities.Investment>
  {
    Task<bool> SaveAsync();
  }
}
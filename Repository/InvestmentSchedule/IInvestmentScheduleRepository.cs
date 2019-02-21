using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.InvestmentSchedule
{
  public interface IInvestmentScheduleRepository : IRepository<Entities.InvestmentSchedule>
  {
    Task<bool> SaveAsync();
  }
}
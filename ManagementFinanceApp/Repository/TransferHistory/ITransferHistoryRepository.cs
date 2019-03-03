using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.TransferHistory
{
  public interface ITransferHistoryRepository : IRepository<Entities.TransferHistory>
  {
    Task<bool> SaveAsync();
  }
}
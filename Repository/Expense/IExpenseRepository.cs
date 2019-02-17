using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.Expense
{
  public interface IExpenseRepository : IRepository<Entities.Expense>
  {
    Task<bool> SaveAsync();
  }
}
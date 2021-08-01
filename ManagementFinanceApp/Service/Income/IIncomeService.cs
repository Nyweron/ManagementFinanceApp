using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.Income
{
  public interface IIncomeService
  {
    Task<IEnumerable<Entities.Income>> GetAllAsync();
    Task<Entities.Income> GetAsync(int categoryIncomeId);
    Task<bool> AddIncome(Models.Income income);
    Task<bool> RemoveAsync(Entities.Income income);
    Task<bool> EditIncome(Models.Income income, int id);
  }
}

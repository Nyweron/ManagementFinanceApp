using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository
{
  public interface IRepository<TEntitiy> where TEntitiy : class
  {
    TEntitiy Get(int id);
    Task<TEntitiy> GetAsync(int id);
    IEnumerable<TEntitiy> GetAll();
    Task<IEnumerable<TEntitiy>> GetAllAsync();
    IEnumerable<TEntitiy> Find(Expression<Func<TEntitiy, bool>> predicate);
    void Add(TEntitiy entity);
    Task AddAsync(TEntitiy entity);
    void AddRange(IEnumerable<TEntitiy> entities);
    Task AddRangeAsync(IEnumerable<TEntitiy> entities);
    void Remove(TEntitiy entity);
    void RemoveRange(IEnumerable<TEntitiy> entities);
  }
}
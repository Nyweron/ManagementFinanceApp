using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Repository.Expense;
using Microsoft.Extensions.Logging;

namespace ManagementFinanceApp.Service.Expense
{
  public class ExpenseService : IExpenseService
  {
    private IExpenseRepository _expenseRepository;
    private IExpenseAdapter _expenseAdapter;
    private IMapper _mapper;
    private ILogger _logger;

    public ExpenseService(IExpenseRepository expenseRepository,
      IMapper mapper,
      ILogger logger, 
      IExpenseAdapter expenseAdapter)
    {
      _expenseRepository = expenseRepository;
      _mapper = mapper;
      _logger = logger;
      _expenseAdapter = expenseAdapter;
    }
    public async Task<bool> AddExpense(Models.Expense expense)
    {
      var expenseEntity = _mapper.Map<Entities.Expense>(expense);
      await _expenseRepository.AddAsync(expenseEntity);

      if (!await _expenseRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditExpense(Models.Expense expenseRequest, int id)
    {
      // Get stock item by id
      var expenseFromDB = await _expenseRepository.GetAsync(id);

      // Validate if entity exists
      if (expenseFromDB == null) { return false; }

      // Set changes to entity
      var dto = _mapper.Map<Models.Expense, Entities.Expense>(expenseRequest, expenseFromDB);

      try
      {
        _expenseRepository.Update(dto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Edit Expense catch exception when update row. {ex}");
        return false;
      }

      if (!await _expenseRepository.SaveAsync())
      {
        _logger.LogError($"Edit Expense is not valid. Error in SaveAsync(). When accessing to ExpenseService/Edit");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Models.ExpenseList>> GetAllAdaptAsync()
    {
      var expense = await _expenseAdapter.AdaptExpense();
      var orderByIds = expense.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }

    public async Task<IEnumerable<Entities.Expense>> GetAllAsync()
    {
      var expenses = await _expenseRepository.GetAllAsync();
      var orderByIds = expenses.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }

    public async Task<Entities.Expense> GetAsync(int expenseId)
    {
      return await _expenseRepository.GetAsync(expenseId);
    }

    public async Task<bool> RemoveAsync(Entities.Expense expense)
    {
      return await _expenseRepository.RemoveAsync(expense);
    }
  }
}
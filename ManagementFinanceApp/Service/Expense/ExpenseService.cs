using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.Expense;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Service.Expense
{
  public class ExpenseService : IExpenseService
  {
    private IExpenseRepository _expenseRepository;
    private IMapper _mapper;
    public ExpenseService(IExpenseRepository expenseRepository,
      IMapper mapper)
    {
      _expenseRepository = expenseRepository;
      _mapper = mapper;
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
      //TODO, check this, search better approach...

      // expenseRequest.HowMuch
      // expenseRequest.CategorySavingId
      // expenseRequest.CategoryExpenseId
      // expenseRequest.Date
      // expenseRequest.UserId
      // expenseRequest.Comment

      if (expenseRequest != null &&
        expenseRequest.Comment != null &&
        expenseRequest.Comment.Trim().Length != 0)
      {
        expenseFromDB.Comment = expenseRequest.Comment;
      }

      var dto = _mapper.Map<Entities.Expense>(expenseRequest);

      try
      {
        //await _expenseRepository.UpdateAsync(dto);
        _expenseRepository.Update(dto);
      }
      catch (Exception ex)
      {
        var x = ex;
        return false;
      }

      if (!await _expenseRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Entities.Expense>> GetAllAsync()
    {
      var x = await _expenseRepository.GetAllAsync();
      var orderByIds = x.OrderBy(o => o.Id).ToList();
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
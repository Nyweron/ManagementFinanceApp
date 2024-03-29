using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.CategoryExpense;

namespace ManagementFinanceApp.Service.CategoryExpense
{
  public class CategoryExpenseService : ICategoryExpenseService
  {
    private ICategoryExpenseRepository _categoryExpenseRepository;
    private IMapper _mapper;
    public CategoryExpenseService(ICategoryExpenseRepository categoryExpenseRepository,
      IMapper mapper
    )
    {
      _categoryExpenseRepository = categoryExpenseRepository;
      _mapper = mapper;
    }

    public async Task<bool> AddCategoryExpense(Models.CategoryExpense categoryExpense)
    {
      categoryExpense.Id = GenerateNextId().Result;

      var categoryExpenseEntity = _mapper.Map<Entities.CategoryExpense>(categoryExpense);
      await _categoryExpenseRepository.AddAsync(categoryExpenseEntity);

      if (!await _categoryExpenseRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditCategoryExpense(Models.CategoryExpense categoryExpenseRequest, int id)
    {
      // Get stock item by id
      var categoryExpenseFromDB = await _categoryExpenseRepository.GetAsync(id);

      // Validate if entity exists
      if (categoryExpenseFromDB == null) { return false; }

      // Set changes to entity
      //TODO, check this, search better approach...
      if (categoryExpenseRequest != null &&
        categoryExpenseRequest.Description != null &&
        categoryExpenseRequest.Description.Trim().Length != 0)
      {
        categoryExpenseFromDB.Description = categoryExpenseRequest.Description;
      }

      if (!await _categoryExpenseRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Entities.CategoryExpense>> GetAllAsync()
    {
      return await _categoryExpenseRepository.GetAllAsync();
    }

    public async Task<Entities.CategoryExpense> GetAsync(int categoryExpenseId)
    {
      return await _categoryExpenseRepository.GetAsync(categoryExpenseId);
    }

    public async Task<bool> RemoveAsync(Entities.CategoryExpense categoryExpense)
    {
      return await _categoryExpenseRepository.RemoveAsync(categoryExpense);
    }

    private async Task<int> GenerateNextId()
    {
      var expenses = await _categoryExpenseRepository.GetAllAsync();
      var highestId = expenses.Max(x => x.Id);
      return highestId + 1;
    }

  }
}
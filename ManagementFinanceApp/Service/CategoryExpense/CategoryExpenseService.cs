using System.Collections.Generic;
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

    public async Task<bool> AddCategoryExpense(List<Models.CategoryExpense> categoryExpense)
    {
      var categoryExpenseEntity = _mapper.Map<List<Entities.CategoryExpense>>(categoryExpense);
      await _categoryExpenseRepository.AddRangeAsync(categoryExpenseEntity);

      if (!await _categoryExpenseRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public Task<bool> EditCategoryExpense(Models.CategoryExpense categoryExpense, int id)
    {
      throw new System.NotImplementedException();
    }

  }
}
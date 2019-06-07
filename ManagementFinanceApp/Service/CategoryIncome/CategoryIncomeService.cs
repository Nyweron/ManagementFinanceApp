using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.CategoryIncome;

namespace ManagementFinanceApp.Service.CategoryIncome
{
  public class CategoryIncomeService : ICategoryIncomeService
  {
    private ICategoryIncomeRepository _categoryIncomeRepository;
    private IMapper _mapper;
    public CategoryIncomeService(ICategoryIncomeRepository categoryIncomeRepository,
      IMapper mapper
    )
    {
      _categoryIncomeRepository = categoryIncomeRepository;
      _mapper = mapper;
    }

    public async Task<bool> AddCategoryIncome(List<Models.CategoryIncome> categoryIncome)
    {
      var categoryIncomeEntity = _mapper.Map<List<Entities.CategoryIncome>>(categoryIncome);
      await _categoryIncomeRepository.AddRangeAsync(categoryIncomeEntity);

      if (!await _categoryIncomeRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditCategoryIncome(Models.CategoryIncome categoryIncomeRequest, int id)
    {
      // Get stock item by id
      var categoryIncomeFromDB = await _categoryIncomeRepository.GetAsync(id);

      // Validate if entity exists
      if (categoryIncomeFromDB == null) { return false; }

      // Set changes to entity
      //TODO, check this, search better approach...
      if (categoryIncomeRequest != null &&
        categoryIncomeRequest.Description != null &&
        categoryIncomeRequest.Description.Trim().Length != 0)
      {
        categoryIncomeFromDB.Description = categoryIncomeRequest.Description;
      }

      if (!await _categoryIncomeRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Entities.CategoryIncome>> GetAllAsync()
    {
      return await _categoryIncomeRepository.GetAllAsync();
    }

    public async Task<Entities.CategoryIncome> GetAsync(int categoryIncomeId)
    {
      return await _categoryIncomeRepository.GetAsync(categoryIncomeId);
    }

    public async Task<bool> RemoveAsync(Entities.CategoryIncome categoryIncome)
    {
      return await _categoryIncomeRepository.RemoveAsync(categoryIncome);
    }

  }
}
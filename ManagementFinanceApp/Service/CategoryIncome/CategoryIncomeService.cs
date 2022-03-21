using System.Collections.Generic;
using System.Linq;
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

    public async Task<bool> AddCategoryIncome(Models.CategoryIncome categoryIncome)
    {
      categoryIncome.Id = GenerateNextId().Result;

      var categoryIncomeEntity = _mapper.Map<Entities.CategoryIncome>(categoryIncome);
      await _categoryIncomeRepository.AddAsync(categoryIncomeEntity);

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

    private async Task<int> GenerateNextId()
    {
      var incomes = await _categoryIncomeRepository.GetAllAsync();
      var highestId = incomes.Max(x => x.Id);
      return highestId + 1;
    }

  }
}
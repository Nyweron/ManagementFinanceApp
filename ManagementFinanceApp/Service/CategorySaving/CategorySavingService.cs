using AutoMapper;
using ManagementFinanceApp.Repository.CategorySaving;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.CategorySaving
{
  public class CategorySavingService : ICategorySavingService
  {

    private ICategorySavingRepository _categorySavingRepository;

    private IMapper _mapper;

    public CategorySavingService(ICategorySavingRepository categorySavingRepository,
                                 IMapper mapper)
    {
      _categorySavingRepository = categorySavingRepository;
      _mapper = mapper;
    }

    public async Task<bool> AddCategorySaving(Models.CategorySaving categorySaving)
    {
      categorySaving.Id = GenerateNextId().Result;

      var savingEntity = _mapper.Map<Entities.CategorySaving>(categorySaving);
      await _categorySavingRepository.AddAsync(savingEntity);

      if (!await _categorySavingRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public Task<bool> EditCategorySaving(Models.CategorySaving categorySaving, int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<IEnumerable<Entities.CategorySaving>> GetAllAsync()
    {
      return await _categorySavingRepository.GetAllAsync();
    }

    public Task<Entities.CategorySaving> GetAsync(int categorySavingId)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> RemoveAsync(Entities.CategorySaving categorySaving)
    {
      throw new System.NotImplementedException();
    }

    private async Task<int> GenerateNextId()
    {
      var savings = await _categorySavingRepository.GetAllAsync();
      var highestId = savings.Max(x => x.Id);
      return highestId + 1;
    }
  }
}

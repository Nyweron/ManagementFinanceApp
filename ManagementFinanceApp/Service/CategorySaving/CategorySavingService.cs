using AutoMapper;
using ManagementFinanceApp.Repository.CategorySaving;
using System.Collections.Generic;
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

    public Task<bool> AddCategorySaving(List<Models.CategorySaving> categorySaving)
    {
      throw new System.NotImplementedException();
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
  }
}

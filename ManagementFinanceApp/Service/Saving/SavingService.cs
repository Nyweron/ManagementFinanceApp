using AutoMapper;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Models.List;
using ManagementFinanceApp.Repository.Saving;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.Saving
{
  public class SavingService : ISavingService
  {
    private ISavingRepository _savingRepository;
    private ISavingAdapter _savingAdapter;
    private IMapper _mapper;
    private ILogger _logger;

    public SavingService(ISavingRepository savingRepository,
                         ISavingAdapter savingAdapter,
                         IMapper mapper,
                         ILogger logger)
    {
      _savingRepository = savingRepository;
      _savingAdapter = savingAdapter;
      _mapper = mapper;
      _logger = logger;
    }

    public async Task<bool> AddSaving(Models.Saving saving)
    {
      saving.Id = GenerateNextId().Result;

      var savingEntity = _mapper.Map<Entities.Saving>(saving);
      await _savingRepository.AddAsync(savingEntity);

      if (!await _savingRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditSaving(Models.Saving saving, int id)
    {
      // Get stock item by id
      var savingFromDB = await _savingRepository.GetAsync(id);

      // Validate if entity exists
      if (savingFromDB == null) { return false; }

      // Set changes to entity
      var dto = _mapper.Map<Models.Saving, Entities.Saving>(saving, savingFromDB);

      try
      {
        _savingRepository.Update(dto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Edit Saving catch exception when update row. {ex}");
        return false;
      }

      if (!await _savingRepository.SaveAsync())
      {
        _logger.LogError($"Edit Saving is not valid. Error in SaveAsync(). When accessing to SavingService/Edit");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<SavingList>> GetAllAdaptAsync()
    {
      var saving = await _savingAdapter.AdaptSaving();
      var orderByIds = saving.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }

    public async Task<IEnumerable<Entities.Saving>> GetAllAsync()
    {
      var x = await _savingRepository.GetAllAsync();
      var orderByIds = x.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }

    public async Task<Entities.Saving> GetAsync(int categorySavingId)
    {
      return await _savingRepository.GetAsync(categorySavingId);
    }

    public async Task<bool> RemoveAsync(Entities.Saving saving)
    {
      return await _savingRepository.RemoveAsync(saving);
    }

    private async Task<int> GenerateNextId()
    {
      var savings = await _savingRepository.GetAllAsync();
      var highestId = savings.Max(x => x.Id);
      return highestId + 1;
    }
  }
}

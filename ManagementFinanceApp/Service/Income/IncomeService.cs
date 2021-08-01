﻿using AutoMapper;
using ManagementFinanceApp.Repository.Income;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Service.Income
{
  public class IncomeService : IIncomeService
  {
    private IIncomeRepository _incomeRepository;
    private IMapper _mapper;
    private ILogger _logger;

    public IncomeService(IIncomeRepository incomeRepository,
      IMapper mapper,
      ILogger logger)
    {
      _incomeRepository = incomeRepository;
      _mapper = mapper;
      _logger = logger;
    }
    public async Task<bool> AddIncome(Models.Income income)
    {
      var incomeEntity = _mapper.Map<Entities.Income>(income);
      // await _incomeRepository.AddAsync(incomeEntity);

      if (!await _incomeRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditIncome(Models.Income incomeRequest, int id)
    {
      // Get stock item by id
      var incomeFromDB = await _incomeRepository.GetAsync(id);

      // Validate if entity exists
      if (incomeFromDB == null) { return false; }

      // Set changes to entity
      var dto = _mapper.Map<Models.Income, Entities.Income>(incomeRequest, incomeFromDB);

      try
      {
        _incomeRepository.Update(dto);
      }
      catch (Exception ex)
      {
        _logger.LogError($"Edit Income catch exception when update row. {ex}");
        return false;
      }

      if (!await _incomeRepository.SaveAsync())
      {
        _logger.LogError($"Edit Income is not valid. Error in SaveAsync(). When accessing to IncomeService/Edit");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Entities.Income>> GetAllAsync()
    {
      var x = await _incomeRepository.GetAllAsync();
      var orderByIds = x.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }

    public async Task<Entities.Income> GetAsync(int incomeId)
    {
      return await _incomeRepository.GetAsync(incomeId);
    }

    public async Task<bool> RemoveAsync(Entities.Income income)
    {
      return await _incomeRepository.RemoveAsync(income);
    }
  }
}

using AutoMapper;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.CategoryIncome;
using ManagementFinanceApp.Repository.CategorySaving;
using ManagementFinanceApp.Repository.Income;
using ManagementFinanceApp.Repository.User;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class IncomeAdapter : IIncomeAdapter
  {
    private readonly IIncomeRepository _incomeRepository;
    private readonly ICategoryIncomeRepository _categoryIncomeRepository;
    private readonly ICategorySavingRepository _categorySavingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public IncomeAdapter(IIncomeRepository incomeRepository,
                         ICategoryIncomeRepository categoryIncomeRepository,
                         ICategorySavingRepository categorySavingRepository,
                         IUserRepository userRepository,
                         IMapper mapper,
                         ILogger logger)
    {
      _incomeRepository = incomeRepository;
      _categoryIncomeRepository = categoryIncomeRepository;
      _categorySavingRepository = categorySavingRepository;
      _userRepository = userRepository;
      _mapper = mapper;
      _logger = logger;
    }

    public async Task<IEnumerable<Models.List.IncomeList>> AdaptIncome()
    {
      var incomesModelList = new List<Models.List.IncomeList>();

      var incomes = await _incomeRepository.GetAllAsync();

      foreach (var income in incomes)
      {
        var categoryIncome = await _categoryIncomeRepository.GetAsync(income.CategoryIncomeId);
        if (categoryIncome == null)
        {
          continue;
        }

        var categorySaving = await _categorySavingRepository.GetAsync(income.CategorySavingId);
        if (categorySaving == null)
        {
          continue;
        }

        var user = await _userRepository.GetAsync(income.UserId);
        if (user == null)
        {
          continue;
        }

        incomesModelList.Add(new Models.List.IncomeList
        {
          Id = income.Id,
          Attachment = income.Attachment,
          CategoryIncomeId = income.CategoryIncomeId,
          CategoryIncomeDescription = categoryIncome.Description,
          CategorySavingId = income.CategorySavingId,
          CategorySavingDescription = categorySaving.Description,
          Comment = income.Comment,
          Date = income.Date,
          HowMuch = income.HowMuch,
          UserId = income.UserId,
          UserDescription = user.FirstName + " " + user.LastName,
          StandingOrder = income.StandingOrder,
        });

      }

      return incomesModelList;



    }
  }
}

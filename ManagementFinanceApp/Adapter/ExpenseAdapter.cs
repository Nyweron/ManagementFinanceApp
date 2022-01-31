using AutoMapper;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Repository.CategorySaving;
using ManagementFinanceApp.Repository.Expense;
using ManagementFinanceApp.Repository.User;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class ExpenseAdapter : IExpenseAdapter
  {
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryExpenseRepository _categoryExpenseRepository;
    private readonly ICategorySavingRepository _categorySavingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ExpenseAdapter(IExpenseRepository expenseRepository,
                          ICategoryExpenseRepository categoryExpenseRepository,
                          ICategorySavingRepository categorySavingRepository,
                          IMapper mapper,
                          IUserRepository userRepository)
    {
      _expenseRepository = expenseRepository;
      _categoryExpenseRepository = categoryExpenseRepository;
      _categorySavingRepository = categorySavingRepository;
      _mapper = mapper;
      _userRepository = userRepository;
    }

    public async Task<IEnumerable<Models.ExpenseList>> AdaptExpense()
    {
      var expensesModelList = new List<Models.ExpenseList>();

      var expenses = await _expenseRepository.GetAllAsync();
      foreach (var expense in expenses)
      {
        var categoryExpense = await _categoryExpenseRepository.GetAsync(expense.CategoryExpenseId);
        if (categoryExpense == null)
        {
          continue;
        }

        var categorySaving = await _categorySavingRepository.GetAsync(expense.CategorySavingId);
        if (categorySaving == null)
        {
          continue;
        }

        var user = await _userRepository.GetAsync(expense.UserId);
        if (user == null)
        { 
          continue; 
        }

        expensesModelList.Add(new Models.ExpenseList
        {
          Id = expense.Id,
          Attachment = expense.Attachment,
          CategoryExpenseId = expense.CategoryExpenseId,
          CategoryExpenseDescription = categoryExpense.Description,
          CategorySavingId = expense.CategorySavingId,
          CategorySavingDescription = categorySaving.Description,
          Comment = expense.Comment,
          Date = expense.Date,
          HowMuch = expense.HowMuch,
          UserId = expense.UserId,
          UserDescription = user.FirstName + " " + user.LastName,
          StandingOrder = expense.StandingOrder,
        });

      }

      return expensesModelList;
    }
  }
}

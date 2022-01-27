using AutoMapper;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Repository.CategorySaving;
using ManagementFinanceApp.Repository.Expense;
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
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ExpenseAdapter(IExpenseRepository expenseRepository,
                          ICategoryExpenseRepository categoryExpenseRepository,
                          ICategorySavingRepository categorySavingRepository,
                          IMapper mapper)
    {
      _expenseRepository = expenseRepository;
      _categoryExpenseRepository = categoryExpenseRepository;
      _categorySavingRepository = categorySavingRepository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<Models.Expense>> AdaptExpense()
    {
      var expensesModelList = new List<Models.Expense>();

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

        expensesModelList.Add(new Models.Expense
        {
          Id = expense.Id,
          Attachment = expense.Attachment,
          CategoryExpenseId = expense.CategoryExpenseId,
          CategorySavingId = expense.CategorySavingId,
          Comment = expense.Comment,
          Date = expense.Date,
          HowMuch = expense.HowMuch,
          UserId = expense.UserId,
          StandingOrder = expense.StandingOrder,
          CategoryExpenseDescription = categoryExpense.Description,
          CategorySavingDescription = categorySaving.Description,
        });

      }

      return expensesModelList;
    }
  }
}

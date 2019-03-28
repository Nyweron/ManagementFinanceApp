using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using ManagementFinanceApp.Controllers;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Repository;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Service.CategoryExpense;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Controllers
{
  [TestFixture]
  public class CategoryExpenseControllerTest
  {

    IMapper mapper = AutoMapperConfig.GetMapper();

    [Test]
    public async Task GetAllCategoryExpenses_ShouldReturnAllCategoryExpensesAsync()
    {
      // arrange
      var expectedNumberOfCategoryExpensesList = 4;
      var testModelCategoryExpense = new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 };
      var categoryExpensesListTest = GetTestCategoryExpenses();

      var mock = new Mock<ICategoryExpenseRepository>();
      mock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(categoryExpensesListTest));

      var controller = new CategoryExpenseController(mock.Object, null);

      //act
      var okObjectResult = await controller.GetAll() as OkObjectResult;
      var result = okObjectResult.Value as List<Entities.CategoryExpense>;

      //assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(expectedNumberOfCategoryExpensesList, result.Count(), "Expected Number Of CategoryExpenses List");
      Assert.AreEqual(testModelCategoryExpense.Id, result[0].Id, "Id is not equal");
    }

    private IEnumerable<Entities.CategoryExpense> GetTestCategoryExpenses()
    {
      var testCategoryExpense = new List<Entities.CategoryExpense>();
      testCategoryExpense.Add(new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });
      testCategoryExpense.Add(new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 });
      testCategoryExpense.Add(new Entities.CategoryExpense { Id = 3, Description = "CategoryeExpense3", IsDeleted = false, Weight = 3, CategoryGroupId = 0 });
      testCategoryExpense.Add(new Entities.CategoryExpense { Id = 4, Description = "CategoryeExpense4", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });

      return testCategoryExpense;
    }

  }
}
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
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Controllers
{
  [TestFixture]
  public class CategoryExpenseControllerTest
  {

    public class OrganizationProfile : Profile
    {
      public OrganizationProfile()
      {
        CreateMap<Models.CategoryExpense, Entities.CategoryExpense>();
        CreateMap<Entities.CategoryExpense, Models.CategoryExpense>();
        // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
      }
    }

    [Test]
    public async Task GetCategoryExpenses_ShouldReturnAllCategoryExpensesAsync()
    {
      // arrange
      var expectedNumberOfCategoryExpensesList = 4;
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<OrganizationProfile>();
      });
      IMapper mapper = config.CreateMapper();
      var testModelCategoryExpense = new Models.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 };

      // var mappedObject = mapper.Map<Entities.CategoryExpense>(testModelCategoryExpense);
      var testCategoryExpenses = GetTestCategoryExpenses();
      var mock = new Mock<ICategoryExpenseRepository>();
      mock.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(testCategoryExpenses));
      var controller = new CategoryExpenseController(mock.Object, mapper);

      //act
      var okObjectResult = await controller.GetAll() as OkObjectResult;
      var result = okObjectResult.Value as List<Entities.CategoryExpense>;

      //assert
      Assert.NotNull(okObjectResult, "Ok Object Result is null");
      Assert.AreEqual(expectedNumberOfCategoryExpensesList, result.Count(), "Expected Number Of CategoryExpenses List");
      Assert.AreEqual(testModelCategoryExpense.Id, result[0].Id, "Id doesnt equal");

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
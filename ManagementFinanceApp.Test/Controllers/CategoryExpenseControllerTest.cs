using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
    private Entities.CategoryExpense categoryExpenseObj;
    private List<Models.CategoryExpense> categoryExpenseList;
    private Mock<ICategoryExpenseRepository> mockCategoryExpenseRepository;
    private Mock<ICategoryExpenseService> mockCategoryExpenseService;
    private int expectedIdOfCategoryExpense;

    [SetUp]
    public void Setup()
    {
      categoryExpenseObj = new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      mockCategoryExpenseRepository = new Mock<ICategoryExpenseRepository>();
      mockCategoryExpenseService = new Mock<ICategoryExpenseService>();
      expectedIdOfCategoryExpense = 2;
    }

    [Test]
    public async Task GetAllCategoryExpenses_ShouldReturnAllCategoryExpensesAsync()
    {
      // Arrange
      var expectedNumberOfCategoryExpensesList = 4;
      var categoryExpensesList = GetCategoryExpensesList();

      mockCategoryExpenseRepository.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(categoryExpensesList));

      var controller = new CategoryExpenseController(mockCategoryExpenseRepository.Object, null);

      // Act
      var okObjectResult = await controller.GetAll() as OkObjectResult;
      var result = okObjectResult.Value as List<Entities.CategoryExpense>;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(expectedNumberOfCategoryExpensesList, result.Count(), "Expected Number Of CategoryExpenses List");
      Assert.AreEqual(categoryExpenseObj.Id, result[1].Id, "Id is not equal");
    }

    [Test]
    public async Task GetByIdCategoryExpenses_ShouldReturnOneCategoryExpenseAsync()
    {
      // Arrange
      var secondItemFromList = 1;
      var categoryExpenseTest = GetCategoryExpensesList().ToList() [secondItemFromList];

      mockCategoryExpenseRepository.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense))
        .Returns(Task.FromResult(categoryExpenseTest));

      var controller = new CategoryExpenseController(mockCategoryExpenseRepository.Object, null);

      // Act
      var okObjectResult = await controller.Get(expectedIdOfCategoryExpense) as OkObjectResult;
      var result = okObjectResult.Value as Entities.CategoryExpense;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(categoryExpenseObj.Id, result.Id, "Id is not equal");
    }

    [Test]
    public async Task DeleteByIdCategoryExpenses_ShouldDeleteOneCategoryExpense()
    {
      // Arrange
      var secondItemFromList = 1;
      var categoryExpenseTest = GetCategoryExpensesList().ToList() [secondItemFromList];

      // Act
      mockCategoryExpenseRepository.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense)).Returns(Task.FromResult(categoryExpenseObj));
      mockCategoryExpenseRepository.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(true));

      var controller = new CategoryExpenseController(mockCategoryExpenseRepository.Object, null);
      var noContentResult = await controller.Delete(expectedIdOfCategoryExpense) as NoContentResult;

      // Assert
      Assert.NotNull(noContentResult, "noContentResult is null");
      Assert.AreEqual(noContentResult.StatusCode, 204, "delete is not works");
    }

    [Test]
    public async Task DeleteByIdCategoryExpenses_ShouldReturnNotFoundWhenGetAsync()
    {
      // Arrange

      // Act
      mockCategoryExpenseRepository.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(true));

      var controller = new CategoryExpenseController(mockCategoryExpenseRepository.Object, null);
      var notFoundResult = await controller.Delete(expectedIdOfCategoryExpense) as NotFoundResult;

      // Assert
      Assert.AreEqual(404, notFoundResult.StatusCode, "Not found result, not works. Method Delete");
    }

    [Test]
    public async Task DeleteByIdCategoryExpenses_ShouldReturnInternalServerErrorCategoryExpense()
    {
      // Arrange

      // Act
      mockCategoryExpenseRepository.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense)).Returns(Task.FromResult(categoryExpenseObj));
      mockCategoryExpenseRepository.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(false));

      var controller = new CategoryExpenseController(mockCategoryExpenseRepository.Object, null);
      var noContentResult = await controller.Delete(expectedIdOfCategoryExpense) as ObjectResult;

      // Assert
      Assert.NotNull(noContentResult, "GetAsync returns null object in method Delete");
      Assert.AreEqual(noContentResult.StatusCode, 500, "Internal server error in method Delete");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldReturnBadRequestObjectIsNull()
    {
      // Arrange
      var categoryExpenseList = new List<Models.CategoryExpense>();

      // Act
      var controller = new CategoryExpenseController(null, null);
      var badRequestResult = await controller.Post(categoryExpenseList) as BadRequestResult;

      // Assert
      Assert.AreEqual(400, badRequestResult.StatusCode, "Badrequest does not works. Method post");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldCreateCategoryExpense()
    {
      // Arrange
      var categoryExpenseListObj = new List<Models.CategoryExpense>() { new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 } };

      // Act
      mockCategoryExpenseService.Setup(repo => repo.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>())).Returns(Task.FromResult(true));

      var controller = new CategoryExpenseController(null, mockCategoryExpenseService.Object);
      var objectResult = await controller.Post(categoryExpenseListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(201, objectResult.StatusCode, "CategoryExpense Created does not works. Method post");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldNotCreateCategoryExpense()
    {
      // Arrange
      var categoryExpenseListObj = new List<Models.CategoryExpense>() { new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 } };

      // Act
      mockCategoryExpenseService.Setup(repo => repo.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>())).Returns(Task.FromResult(false));

      var controller = new CategoryExpenseController(null, mockCategoryExpenseService.Object);
      var objectResult = await controller.Post(categoryExpenseListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "CategoryExpense StatusCode500 does not works. Method post");
    }

    private IEnumerable<Entities.CategoryExpense> GetCategoryExpensesList()
    {
      var categoryExpenseListObj = new List<Entities.CategoryExpense>();
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 3, Description = "CategoryeExpense3", IsDeleted = false, Weight = 3, CategoryGroupId = 0 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 4, Description = "CategoryeExpense4", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });

      return categoryExpenseListObj;
    }

  }
}
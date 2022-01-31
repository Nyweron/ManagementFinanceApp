using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Controllers;
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
    private Models.CategoryExpense categoryExpenseModelObj;
    private Mock<ICategoryExpenseRepository> mockCategoryExpenseRepository;
    private Mock<ICategoryExpenseAdapter> mockiCategoryExpenseAdapter;
    private Mock<ICategoryExpenseService> mockCategoryExpenseService;
    private int expectedIdOfCategoryExpense;
    private List<Models.CategoryExpense> categoryExpenseListObj;

    private IEnumerable<Entities.CategoryExpense> GetCategoryExpensesList()
    {
      var categoryExpenseListObj = new List<Entities.CategoryExpense>();
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 3, Description = "CategoryeExpense3", IsDeleted = false, Weight = 3, CategoryGroupId = 0 });
      categoryExpenseListObj.Add(new Entities.CategoryExpense { Id = 4, Description = "CategoryeExpense4", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });

      return categoryExpenseListObj;
    }

    [SetUp]
    public void Setup()
    {
      categoryExpenseObj = new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelObj = new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      mockCategoryExpenseRepository = new Mock<ICategoryExpenseRepository>();
      mockCategoryExpenseService = new Mock<ICategoryExpenseService>();
      mockiCategoryExpenseAdapter = new Mock<ICategoryExpenseAdapter>();
      expectedIdOfCategoryExpense = 2;
      categoryExpenseListObj = new List<Models.CategoryExpense>() { new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 } };
    }

    [Test]
    public async Task GetAllCategoryExpenses_ShouldReturnAllCategoryExpensesAsync()
    {
      // Arrange
      var expectedNumberOfCategoryExpensesList = 4;
      var categoryExpensesList = GetCategoryExpensesList();

      mockCategoryExpenseService.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(categoryExpensesList));

      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

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
      var categoryExpenseTestIndex = 1;
      var categoryExpenseTest = GetCategoryExpensesList().ToList() [categoryExpenseTestIndex];

      mockCategoryExpenseService.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense))
        .Returns(Task.FromResult(categoryExpenseTest));

      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

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
      var categoryExpenseTestIndex = 1;
      var categoryExpenseTest = GetCategoryExpensesList().ToList() [categoryExpenseTestIndex];

      mockCategoryExpenseService.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense)).Returns(Task.FromResult(categoryExpenseObj));
      mockCategoryExpenseService.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(true));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfCategoryExpense) as NoContentResult;

      // Assert
      Assert.NotNull(noContentResult, "noContentResult is null");
      Assert.AreEqual(noContentResult.StatusCode, 204, "delete is not works");
    }

    [Test]
    public async Task DeleteByIdCategoryExpenses_ShouldReturnNotFoundWhenGetAsync()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(true));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

      // Act
      var notFoundResult = await controller.Delete(expectedIdOfCategoryExpense) as NotFoundResult;

      // Assert
      Assert.AreEqual(404, notFoundResult.StatusCode, "Not found result, not works. Method Delete");
    }

    [Test]
    public async Task DeleteByIdCategoryExpenses_ShouldReturnInternalServerErrorCategoryExpense()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.GetAsync(expectedIdOfCategoryExpense)).Returns(Task.FromResult(categoryExpenseObj));
      mockCategoryExpenseService.Setup(repo => repo.RemoveAsync(categoryExpenseObj)).Returns(Task.FromResult(false));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfCategoryExpense) as ObjectResult;

      // Assert
      Assert.NotNull(noContentResult, "GetAsync returns null object in method Delete");
      Assert.AreEqual(500, noContentResult.StatusCode, "Internal server error in method Delete");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldReturnBadRequestObjectIsNull()
    {
      // Arrange
      var categoryExpenseList = new List<Models.CategoryExpense>();
      var controller = new CategoryExpenseController(null,null);

      // Act
      var badRequestResult = await controller.Post(categoryExpenseList) as BadRequestResult;

      // Assert
      Assert.AreEqual(400, badRequestResult.StatusCode, "Badrequest does not works. Method post");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldCreateCategoryExpense()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>())).Returns(Task.FromResult(true));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

      // Act
      var objectResult = await controller.Post(categoryExpenseListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(201, objectResult.StatusCode, "CategoryExpense Created does not works. Method post");
    }

    [Test]
    public async Task PostCategoryExpenses_ShouldNotCreateCategoryExpense()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>())).Returns(Task.FromResult(false));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);

      // Act
      var objectResult = await controller.Post(categoryExpenseListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "CategoryExpense StatusCode500 does not works. Method post");
    }

    [Test]
    public async Task PutCategoryExpenses_ShouldReturnBadRequestWhenObjectIsNull()
    {
      // Arrange
      expectedIdOfCategoryExpense = 1;
      var controller = new CategoryExpenseController(null,null);

      // Act
      var objectResult = await controller.Edit(expectedIdOfCategoryExpense, null) as ObjectResult;

      // Assert
      Assert.AreEqual(400, objectResult.StatusCode, "CategoryExpense StatusCode400 does not works. Method put. Object cannot be empty");
    }

    [Test]
    public async Task PutCategoryExpenses_ShouldReturnStatusCode500WhenObjectIsNotUpdated()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.EditCategoryExpense(It.IsAny<Models.CategoryExpense>(), It.IsAny<int>())).Returns(Task.FromResult(false));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);
      expectedIdOfCategoryExpense = 1;

      // Act
      var objectResult = await controller.Edit(expectedIdOfCategoryExpense, categoryExpenseModelObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "CategoryExpense method put. Object was not updated.");
    }

    [Test]
    public async Task PutCategoryExpenses_ShouldReturnStatusCode204WhenObjectIsUpdated()
    {
      // Arrange
      mockCategoryExpenseService.Setup(repo => repo.EditCategoryExpense(It.IsAny<Models.CategoryExpense>(), It.IsAny<int>())).Returns(Task.FromResult(true));
      var controller = new CategoryExpenseController(mockCategoryExpenseService.Object, mockiCategoryExpenseAdapter.Object);
      expectedIdOfCategoryExpense = 1;

      // Act
      var noContentResult = await controller.Edit(expectedIdOfCategoryExpense, categoryExpenseModelObj) as NoContentResult;

      // Assert
      Assert.AreEqual(204, noContentResult.StatusCode, "CategoryExpense method put. Object was not updated.");
    }

  }

}
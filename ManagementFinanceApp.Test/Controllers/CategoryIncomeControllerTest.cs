using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Controllers;
using ManagementFinanceApp.Repository.CategoryIncome;
using ManagementFinanceApp.Service.CategoryIncome;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Controllers
{
  [TestFixture]
  public class CategoryIncomeControllerTest
  {
    IMapper mapper = AutoMapperConfig.GetMapper();
    private Entities.CategoryIncome categoryIncomeObj;
    private Models.CategoryIncome categoryIncomeModelObj;
    private Mock<ICategoryIncomeRepository> mockCategoryIncomeRepository;
    private Mock<ICategoryIncomeService> mockCategoryIncomeService;
    private Mock<ICategoryIncomeAdapter> mockCategoryIncomeAdapter;
    private int expectedIdOfCategoryIncome;
    private List<Models.CategoryIncome> categoryIncomeListObj;

    private IEnumerable<Entities.CategoryIncome> GetCategoryIncomesList()
    {
      var categoryIncomeListObj = new List<Entities.CategoryIncome>();
      categoryIncomeListObj.Add(new Entities.CategoryIncome { Id = 1, Description = "CategoryIncome1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });
      categoryIncomeListObj.Add(new Entities.CategoryIncome { Id = 2, Description = "CategoryIncome2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 });
      categoryIncomeListObj.Add(new Entities.CategoryIncome { Id = 3, Description = "CategoryIncome3", IsDeleted = false, Weight = 3, CategoryGroupId = 0 });
      categoryIncomeListObj.Add(new Entities.CategoryIncome { Id = 4, Description = "CategoryIncome4", IsDeleted = false, Weight = 1, CategoryGroupId = 1 });

      return categoryIncomeListObj;
    }

    [SetUp]
    public void Setup()
    {
      categoryIncomeObj = new Entities.CategoryIncome { Id = 2, Description = "CategoryIncome2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryIncomeModelObj = new Models.CategoryIncome { Id = 2, Description = "CategoryIncome2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      mockCategoryIncomeRepository = new Mock<ICategoryIncomeRepository>();
      mockCategoryIncomeService = new Mock<ICategoryIncomeService>();
      mockCategoryIncomeAdapter = new Mock<ICategoryIncomeAdapter>();
      expectedIdOfCategoryIncome = 2;
      categoryIncomeListObj = new List<Models.CategoryIncome>() { new Models.CategoryIncome { Id = 2, Description = "CategoryIncome2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 } };
    }

    [Test]
    public async Task GetAllCategoryIncomes_ShouldReturnAllCategoryIncomesAsync()
    {
      // Arrange
      var expectedNumberOfCategoryIncomesList = 4;
      var categoryIncomesList = GetCategoryIncomesList();

      mockCategoryIncomeService.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(categoryIncomesList));

      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var okObjectResult = await controller.GetAll() as OkObjectResult;
      var result = okObjectResult.Value as List<Entities.CategoryIncome>;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(expectedNumberOfCategoryIncomesList, result.Count(), "Expected Number Of CategoryIncomes List");
      Assert.AreEqual(categoryIncomeObj.Id, result[1].Id, "Id is not equal");
    }

    [Test]
    public async Task GetByIdCategoryIncomes_ShouldReturnOneCategoryIncomeAsync()
    {
      // Arrange
      var categoryIncomeTestIndex = 1;
      var categoryIncomeTest = GetCategoryIncomesList().ToList() [categoryIncomeTestIndex];

      mockCategoryIncomeService.Setup(repo => repo.GetAsync(expectedIdOfCategoryIncome))
        .Returns(Task.FromResult(categoryIncomeTest));

      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var okObjectResult = await controller.Get(expectedIdOfCategoryIncome) as OkObjectResult;
      var result = okObjectResult.Value as Entities.CategoryIncome;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(categoryIncomeObj.Id, result.Id, "Id is not equal");
    }

    [Test]
    public async Task DeleteByIdCategoryIncomes_ShouldDeleteOneCategoryIncome()
    {
      // Arrange
      var categoryIncomeTestIndex = 1;
      var categoryIncomeTest = GetCategoryIncomesList().ToList() [categoryIncomeTestIndex];

      mockCategoryIncomeService.Setup(repo => repo.GetAsync(expectedIdOfCategoryIncome)).Returns(Task.FromResult(categoryIncomeObj));
      mockCategoryIncomeService.Setup(repo => repo.RemoveAsync(categoryIncomeObj)).Returns(Task.FromResult(true));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfCategoryIncome) as NoContentResult;

      // Assert
      Assert.NotNull(noContentResult, "noContentResult is null");
      Assert.AreEqual(noContentResult.StatusCode, 204, "delete is not works");
    }

    [Test]
    public async Task DeleteByIdCategoryIncomes_ShouldReturnNotFoundWhenGetAsync()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.RemoveAsync(categoryIncomeObj)).Returns(Task.FromResult(true));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var notFoundResult = await controller.Delete(expectedIdOfCategoryIncome) as NotFoundResult;

      // Assert
      Assert.AreEqual(404, notFoundResult.StatusCode, "Not found result, not works. Method Delete");
    }

    [Test]
    public async Task DeleteByIdCategoryIncomes_ShouldReturnInternalServerErrorCategoryIncome()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.GetAsync(expectedIdOfCategoryIncome)).Returns(Task.FromResult(categoryIncomeObj));
      mockCategoryIncomeService.Setup(repo => repo.RemoveAsync(categoryIncomeObj)).Returns(Task.FromResult(false));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfCategoryIncome) as ObjectResult;

      // Assert
      Assert.NotNull(noContentResult, "GetAsync returns null object in method Delete");
      Assert.AreEqual(500, noContentResult.StatusCode, "Internal server error in method Delete");
    }

    [Test]
    public async Task PostCategoryIncomes_ShouldReturnBadRequestObjectIsNull()
    {
      // Arrange
      var categoryIncomeList = new List<Models.CategoryIncome>();
      var controller = new CategoryIncomeController(null, null);

      // Act
      var badRequestResult = await controller.Post(categoryIncomeList) as BadRequestResult;

      // Assert
      Assert.AreEqual(400, badRequestResult.StatusCode, "Badrequest does not works. Method post");
    }

    [Test]
    public async Task PostCategoryIncomes_ShouldCreateCategoryIncome()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.AddCategoryIncome(It.IsAny<List<Models.CategoryIncome>>())).Returns(Task.FromResult(true));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var objectResult = await controller.Post(categoryIncomeListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(201, objectResult.StatusCode, "CategoryIncome Created does not works. Method post");
    }

    [Test]
    public async Task PostCategoryIncomes_ShouldNotCreateCategoryIncome()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.AddCategoryIncome(It.IsAny<List<Models.CategoryIncome>>())).Returns(Task.FromResult(false));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);

      // Act
      var objectResult = await controller.Post(categoryIncomeListObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "CategoryIncome StatusCode500 does not works. Method post");
    }

    [Test]
    public async Task PutCategoryIncomes_ShouldReturnBadRequestWhenObjectIsNull()
    {
      // Arrange
      expectedIdOfCategoryIncome = 1;
      var controller = new CategoryIncomeController(null, mockCategoryIncomeAdapter.Object);

      // Act
      var objectResult = await controller.Edit(expectedIdOfCategoryIncome, null) as ObjectResult;

      // Assert
      Assert.AreEqual(400, objectResult.StatusCode, "CategoryIncome StatusCode400 does not works. Method put. Object cannot be empty");
    }

    [Test]
    public async Task PutCategoryIncomes_ShouldReturnStatusCode500WhenObjectIsNotUpdated()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.EditCategoryIncome(It.IsAny<Models.CategoryIncome>(), It.IsAny<int>())).Returns(Task.FromResult(false));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);
      expectedIdOfCategoryIncome = 1;

      // Act
      var objectResult = await controller.Edit(expectedIdOfCategoryIncome, categoryIncomeModelObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "CategoryIncome method put. Object was not updated.");
    }

    [Test]
    public async Task PutCategoryIncomes_ShouldReturnStatusCode204WhenObjectIsUpdated()
    {
      // Arrange
      mockCategoryIncomeService.Setup(repo => repo.EditCategoryIncome(It.IsAny<Models.CategoryIncome>(), It.IsAny<int>())).Returns(Task.FromResult(true));
      var controller = new CategoryIncomeController(mockCategoryIncomeService.Object, mockCategoryIncomeAdapter.Object);
      expectedIdOfCategoryIncome = 1;

      // Act
      var noContentResult = await controller.Edit(expectedIdOfCategoryIncome, categoryIncomeModelObj) as NoContentResult;

      // Assert
      Assert.AreEqual(204, noContentResult.StatusCode, "CategoryIncome method put. Object was not updated.");
    }

  }

}
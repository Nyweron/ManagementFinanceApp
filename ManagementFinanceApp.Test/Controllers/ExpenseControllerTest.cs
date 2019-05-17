using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Controllers;
using ManagementFinanceApp.Repository.Expense;
using ManagementFinanceApp.Service.Expense;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Controllers
{
  [TestFixture]
  public class ExpenseControllerTest
  {
    IMapper mapper = AutoMapperConfig.GetMapper();
    private Entities.Expense expenseObj;
    private Models.Expense expenseModelObj;
    private Mock<IExpenseRepository> mockExpenseRepository;
    private Mock<IExpenseService> mockExpenseService;
    private int expectedIdOfExpense;
    private List<Models.Expense> expenseListObj;

    private IEnumerable<Entities.Expense> GetExpensesList()
    {
      var expenseListObj = new List<Entities.Expense>();
      expenseListObj.Add(new Entities.Expense { Id = 1, Comment = "Expense1" });
      expenseListObj.Add(new Entities.Expense { Id = 2, Comment = "Expense2" });
      expenseListObj.Add(new Entities.Expense { Id = 3, Comment = "Expense3" });
      expenseListObj.Add(new Entities.Expense { Id = 4, Comment = "Expense4" });

      return expenseListObj;
    }

    [SetUp]
    public void Setup()
    {
      expenseObj = new Entities.Expense { Id = 2, Comment = "Expense2" };
      expenseModelObj = new Models.Expense { Id = 2, Comment = "Expense2" };
      mockExpenseRepository = new Mock<IExpenseRepository>();
      mockExpenseService = new Mock<IExpenseService>();
      expectedIdOfExpense = 2;
      expenseListObj = new List<Models.Expense>() { new Models.Expense { Id = 2, Comment = "Expense2" } };
    }

    [Test]
    public async Task GetAllExpenses_ShouldReturnAllExpensesAsync()
    {
      // Arrange
      var expectedNumberOfExpensesList = 4;
      var expensesList = GetExpensesList();

      mockExpenseService.Setup(repo => repo.GetAllAsync()).Returns(Task.FromResult(expensesList));

      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var okObjectResult = await controller.GetAll() as OkObjectResult;
      var result = okObjectResult.Value as List<Entities.Expense>;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(expectedNumberOfExpensesList, result.Count(), "Expected Number Of Expenses List");
      Assert.AreEqual(expenseObj.Id, result[1].Id, "Id is not equal");
    }

    [Test]
    public async Task GetByIdExpenses_ShouldReturnOneExpenseAsync()
    {
      // Arrange
      var expenseTestIndex = 1;
      var expenseTest = GetExpensesList().ToList() [expenseTestIndex];

      mockExpenseService.Setup(repo => repo.GetAsync(expectedIdOfExpense))
        .Returns(Task.FromResult(expenseTest));

      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var okObjectResult = await controller.Get(expectedIdOfExpense) as OkObjectResult;
      var result = okObjectResult.Value as Entities.Expense;

      // Assert
      Assert.NotNull(okObjectResult, "Ok(ObjectResult) is null");
      Assert.AreEqual(expenseObj.Id, result.Id, "Id is not equal");
    }

    [Test]
    public async Task DeleteByIdExpenses_ShouldDeleteOneExpense()
    {
      // Arrange
      var expenseTestIndex = 1;
      var expenseTest = GetExpensesList().ToList() [expenseTestIndex];

      mockExpenseService.Setup(repo => repo.GetAsync(expectedIdOfExpense)).Returns(Task.FromResult(expenseObj));
      mockExpenseService.Setup(repo => repo.RemoveAsync(expenseObj)).Returns(Task.FromResult(true));
      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfExpense) as NoContentResult;

      // Assert
      Assert.NotNull(noContentResult, "noContentResult is null");
      Assert.AreEqual(noContentResult.StatusCode, 204, "delete is not works");
    }

    [Test]
    public async Task DeleteByIdExpenses_ShouldReturnNotFoundWhenGetAsync()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.RemoveAsync(expenseObj)).Returns(Task.FromResult(true));
      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var notFoundResult = await controller.Delete(expectedIdOfExpense) as NotFoundResult;

      // Assert
      Assert.AreEqual(404, notFoundResult.StatusCode, "Not found result, not works. Method Delete");
    }

    [Test]
    public async Task DeleteByIdExpenses_ShouldReturnInternalServerErrorExpense()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.GetAsync(expectedIdOfExpense)).Returns(Task.FromResult(expenseObj));
      mockExpenseService.Setup(repo => repo.RemoveAsync(expenseObj)).Returns(Task.FromResult(false));
      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var noContentResult = await controller.Delete(expectedIdOfExpense) as ObjectResult;

      // Assert
      Assert.NotNull(noContentResult, "GetAsync returns null object in method Delete");
      Assert.AreEqual(500, noContentResult.StatusCode, "Internal server error in method Delete");
    }

    [Test]
    public async Task PostExpenses_ShouldReturnBadRequestObjectIsNull()
    {
      // Arrange
      var expenseObj = new Models.Expense();
      expenseObj = null;
      var controller = new ExpenseController(null);

      // Act
      var badRequestResult = await controller.Post(expenseObj) as BadRequestResult;

      // Assert
      Assert.AreEqual(400, badRequestResult.StatusCode, "Badrequest does not works. Method post");
    }

    [Test]
    public async Task PostExpenses_ShouldCreateExpense()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.AddExpense(It.IsAny<Models.Expense>()))
        .Returns(Task.FromResult(true));
      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var objectResult = await controller.Post(expenseModelObj) as ObjectResult;

      // Assert
      Assert.AreEqual(201, objectResult.StatusCode, "Expense Created does not works. Method post");
    }

    [Test]
    public async Task PostExpenses_ShouldNotCreateExpense()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.AddExpense(It.IsAny<Models.Expense>()))
        .Returns(Task.FromResult(false));
      var controller = new ExpenseController(mockExpenseService.Object);

      // Act
      var objectResult = await controller.Post(expenseModelObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "Expense StatusCode500 does not works. Method post");
    }

    [Test]
    public async Task PutExpenses_ShouldReturnBadRequestWhenObjectIsNull()
    {
      // Arrange
      expectedIdOfExpense = 1;
      var controller = new ExpenseController(null);

      // Act
      var objectResult = await controller.Edit(expectedIdOfExpense, null) as ObjectResult;

      // Assert
      Assert.AreEqual(400, objectResult.StatusCode, "Expense StatusCode400 does not works. Method put. Object cannot be empty");
    }

    [Test]
    public async Task PutExpenses_ShouldReturnStatusCode500WhenObjectIsNotUpdated()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.EditExpense(It.IsAny<Models.Expense>(), It.IsAny<int>())).Returns(Task.FromResult(false));
      var controller = new ExpenseController(mockExpenseService.Object);
      expectedIdOfExpense = 1;

      // Act
      var objectResult = await controller.Edit(expectedIdOfExpense, expenseModelObj) as ObjectResult;

      // Assert
      Assert.AreEqual(500, objectResult.StatusCode, "Expense method put. Object was not updated.");
    }

    [Test]
    public async Task PutExpenses_ShouldReturnStatusCode204WhenObjectIsUpdated()
    {
      // Arrange
      mockExpenseService.Setup(repo => repo.EditExpense(It.IsAny<Models.Expense>(), It.IsAny<int>())).Returns(Task.FromResult(true));
      var controller = new ExpenseController(mockExpenseService.Object);
      expectedIdOfExpense = 1;

      // Act
      var noContentResult = await controller.Edit(expectedIdOfExpense, expenseModelObj) as NoContentResult;

      // Assert
      Assert.AreEqual(204, noContentResult.StatusCode, "Expense method put. Object was not updated.");
    }

  }

}
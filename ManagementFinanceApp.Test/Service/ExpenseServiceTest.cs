using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Repository.Expense;
using ManagementFinanceApp.Service.Expense;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Service
{
  [TestFixture]
  public class ExpenseServiceTest
  {
    private Mock<IExpenseRepository> mockRepo;
    private Mock<IMapper> mockMapper;
    private Mock<ILogger> mockLogger;
    private Entities.Expense expenseEntityObj;
    private IEnumerable<Entities.Expense> expenseEntityLists;
    private Models.Expense expenseModelObj;
    private List<Models.Expense> expenseModelLists;
    private DbContextOptions<ManagementFinanceAppDbContext> options;
    private ManagementFinanceAppDbContext context;
    private Repository.Repository<Entities.Expense> queryDBInMemory;
    private async Task Seed(ManagementFinanceAppDbContext context)
    {
      var entityExpenses = new []
      {
        new Entities.Expense { Id = 1, Comment = "x1" },
        new Entities.Expense { Id = 2, Comment = "x2" },
        new Entities.Expense { Id = 3, Comment = "x3" },
        new Entities.Expense { Id = 4, Comment = "x4" },
        new Entities.Expense { Id = 5, Comment = "x5" },
        new Entities.Expense { Id = 6, Comment = "x6" }
      };

      await context.Expenses.AddRangeAsync(entityExpenses);
      await context.SaveChangesAsync();
    }

    private async Task InitDatabaseInMemoryTest()
    {
      // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
      options = new DbContextOptionsBuilder<ManagementFinanceAppDbContext>()
        .UseInMemoryDatabase(databaseName: "ManagamentFinanceAppTest")
        .Options;
      context = new ManagementFinanceAppDbContext(options);
      await Seed(context);
      queryDBInMemory = new Repository.Repository<Entities.Expense>(context);
    }

    [SetUp]
    public async Task Setup()
    {
      await InitDatabaseInMemoryTest();

      expenseModelLists = new List<Models.Expense>();
      expenseEntityLists = new List<Entities.Expense>();
      mockRepo = new Mock<IExpenseRepository>();
      mockMapper = new Mock<IMapper>();
      mockLogger = new Mock<ILogger>();
      expenseEntityObj = new Entities.Expense { Id = 2, Comment = "Expense1" };
      expenseModelObj = new Models.Expense { Id = 2, Comment = "Expense2" };
      expenseModelLists.Add(expenseModelObj);
    }

    [TearDown]
    public void Teardown()
    {
      context.Database.EnsureDeleted();
    }

    [Test]
    public async Task ExpenseGetAll_TryingGetAllObjectsFromDB_ShouldReturnSixObjectsInExpenses()
    {
      // Act
      var allExpenses = await queryDBInMemory.GetAllAsync();

      // Assert
      Assert.AreEqual(6, allExpenses.Count(), "GetAllAsync doesn't return objects from Database");
    }

    [Test]
    public async Task ExpenseGet_TryingGetObjectFromDB_ShouldReturnOneObjectInExpenses()
    {
      // Act
      var getExpense = await queryDBInMemory.GetAsync(1);

      // Assert
      Assert.AreEqual(1, getExpense.Id, "GetAsync doesn't return object from Database");
    }

    [Test]
    public async Task ExpenseRemove_TryingRemoveObjectFromDB_ShouldRemoveOneObjectInExpenses()
    {
      // Arrange
      var getExpense = await queryDBInMemory.GetAsync(1);
      var allExpenses = await queryDBInMemory.GetAllAsync();

      // Act
      var isRemoved = await queryDBInMemory.RemoveAsync(getExpense);
      var allExpensesAfterRemoveOne = await queryDBInMemory.GetAllAsync();

      // Assert
      Assert.IsTrue(isRemoved, "RemoveAsync doesn't removed from DataBase");
      Assert.AreEqual(6, allExpenses.Count(), "GetAllAsync doesn't return objects from Database");
      Assert.AreEqual(5, allExpensesAfterRemoveOne.Count(), "GetAllAsync doesn't return objects from Database");
      Assert.AreEqual(1, getExpense.Id, "GetAsync doesn't return object from Database");
    }

    [Test]
    public async Task GetAllExpense_ShouldBeAbleToReturnTwoObjects()
    {
      // Arrange
      expenseEntityLists = new List<Entities.Expense>()
      {
        new Entities.Expense { Id = 8, Comment = "description8" },
          new Entities.Expense { Id = 9, Comment = "description9" }
      }.AsEnumerable();

      mockRepo.Setup(y => y.GetAllAsync())
        .Returns(Task.FromResult(expenseEntityLists));

      var sut = new ExpenseService(mockRepo.Object, null, mockLogger.Object);

      // Act
      var getAllEntities = await sut.GetAllAsync();

      // Assert
      Assert.AreEqual(2, getAllEntities.Count(), "GetAll doesn't return correct Count");
      mockRepo.Verify(x => x.GetAllAsync(), Times.Once, "GetAllAsync should run once");

    }

    [Test]
    public async Task GetExpense_ShouldBeAbleToReturnOneObjectWithIdEquals8()
    {
      // Arrange
      expenseEntityObj = new Entities.Expense { Id = 8, Comment = "description8" };

      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(expenseEntityObj));

      var sut = new ExpenseService(mockRepo.Object, null, mockLogger.Object);

      // Act
      var getAllEntities = await sut.GetAsync(8);

      // Assert
      Assert.AreEqual(8, getAllEntities.Id, "GetAsync doesn't return correct Object");
      mockRepo.Verify(x => x.GetAsync(8), Times.Once, "GetAsync should run once");
    }

    [Test]
    public async Task RemoveExpense_ShouldBeAbleToRemoveOneObjectWithIdEquals1()
    {
      // Arrange
      expenseEntityObj = new Entities.Expense { Id = 1, Comment = "description1" };

      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(expenseEntityObj));
      mockRepo.Setup(y => y.RemoveAsync(It.IsAny<Entities.Expense>()))
        .Returns(Task.FromResult(true));

      var sut = new ExpenseService(mockRepo.Object, null, mockLogger.Object);
      var getEntity = await sut.GetAsync(1);

      // Act

      var isRemoved = await sut.RemoveAsync(getEntity);

      // Assert
      Assert.AreEqual(1, getEntity.Id, "GetAsync doesn't return correct Object");
      Assert.IsTrue(isRemoved, "RemoveAsync doesn't removed correct Object");
      mockRepo.Verify(x => x.GetAsync(1), Times.Once, "GetAsync should run once");
      mockRepo.Verify(x => x.RemoveAsync(getEntity),
        Times.Once, "RemoveAsync should run once");
    }

    [Test]
    public async Task AddExpense_ShouldRunAddRangeAsyncOnlyOnce()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.Expense>>(It.IsAny<List<Models.Expense>>()))
        .Returns(It.IsAny<List<Entities.Expense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()))
        .Returns(() => Task.Run(() => { })).Verifiable();

      var sut = new ExpenseService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

      // Act
      await sut.AddExpense(expenseModelObj);

      // Assert
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()),
        Times.Once, "AddRangeAsync should run once");
    }

    [Test]
    public async Task AddExpense_ShouldBeAbleToAddExpense()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.Expense>>(It.IsAny<List<Models.Expense>>()))
        .Returns(It.IsAny<List<Entities.Expense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new ExpenseService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

      // Act
      var resultOfAddExpense = await sut.AddExpense(expenseModelObj);

      // Assert
      Assert.IsTrue(resultOfAddExpense, "Add and Save should return true.");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task ExpenseAdd_TryingAddNewObjectToDB_ShouldBeAbleReturnIdEquals8()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.Expense>>(It.IsAny<List<Models.Expense>>()))
        .Returns(It.IsAny<List<Entities.Expense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      expenseEntityLists = new List<Entities.Expense>
      {
        new Entities.Expense { Id = 8, Comment = "New category Expense was added" }
      };
      var sut = new ExpenseService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

      // Act
      var resultOfAddExpense = await sut.AddExpense(expenseModelObj);

      await context.Expenses.AddRangeAsync(expenseEntityLists);
      await context.SaveChangesAsync();
      var isAddedNewObject = queryDBInMemory.GetAsync(8);

      // Assert
      Assert.AreEqual(8, isAddedNewObject.Result.Id, "New object was not added, require id=8");
      Assert.IsTrue(resultOfAddExpense, "Add and Save should return true. Object i added to Database");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddExpense_ShouldNotBeAbleToAddExpense()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.Expense>>(It.IsAny<List<Models.Expense>>()))
        .Returns(It.IsAny<List<Entities.Expense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new ExpenseService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

      // Act
      var resultOfAddExpense = await sut.AddExpense(expenseModelObj);

      // Assert
      Assert.IsFalse(resultOfAddExpense, "Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddExpense_ShouldNotBeAbleToAddRandeAsync()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.Expense>>(It.IsAny<List<Models.Expense>>()))
        .Returns(It.IsAny<List<Entities.Expense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()))
        .Returns(() => Task.Run(() => { return false; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new ExpenseService(mockRepo.Object, mockMapper.Object, mockLogger.Object);

      // Act
      var resultOfAddExpense = await sut.AddExpense(expenseModelObj);

      // Assert
      Assert.IsFalse(resultOfAddExpense, "Add and Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.Expense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

  }
}
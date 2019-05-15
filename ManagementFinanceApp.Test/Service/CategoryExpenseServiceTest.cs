using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Controllers;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Service.CategoryExpense;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Service
{
  [TestFixture]
  public class CategoryExpenseServiceTest
  {
    private Mock<ICategoryExpenseRepository> mockRepo;
    private Mock<IMapper> mockMapper;
    private Entities.CategoryExpense categoryExpenseEntityObj;
    private IEnumerable<Entities.CategoryExpense> categoryExpenseEntityLists;
    private Models.CategoryExpense categoryExpenseModelObj;
    private List<Models.CategoryExpense> categoryExpenseModelLists;
    private DbContextOptions<ManagementFinanceAppDbContext> options;
    private ManagementFinanceAppDbContext context;
    private Repository.Repository<Entities.CategoryExpense> queryDBInMemory;
    private async Task Seed(ManagementFinanceAppDbContext context)
    {
      var entityCategoryExpense = new []
      {
        new Entities.CategoryExpense { Id = 1, Description = "x1" },
        new Entities.CategoryExpense { Id = 2, Description = "x2" },
        new Entities.CategoryExpense { Id = 3, Description = "x3" },
        new Entities.CategoryExpense { Id = 4, Description = "x4" },
        new Entities.CategoryExpense { Id = 5, Description = "x5" },
        new Entities.CategoryExpense { Id = 6, Description = "x6" }
      };

      await context.CategoryExpenses.AddRangeAsync(entityCategoryExpense);
      await context.SaveChangesAsync();
    }

    private async Task InitDatabaseInMemoryTest()
    {
      // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
      options = new DbContextOptionsBuilder<ManagementFinanceAppDbContext>()
        .UseInMemoryDatabase(databaseName: "ManagamentFinanceApp")
        .Options;
      context = new ManagementFinanceAppDbContext(options);
      await Seed(context);
      queryDBInMemory = new Repository.Repository<Entities.CategoryExpense>(context);
    }

    [SetUp]
    public async Task Setup()
    {
      await InitDatabaseInMemoryTest();

      categoryExpenseModelLists = new List<Models.CategoryExpense>();
      categoryExpenseEntityLists = new List<Entities.CategoryExpense>();
      mockRepo = new Mock<ICategoryExpenseRepository>();
      mockMapper = new Mock<IMapper>();
      categoryExpenseEntityObj = new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense1", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelObj = new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelLists.Add(categoryExpenseModelObj);
    }

    [TearDown]
    public void Teardown()
    {
      context.Database.EnsureDeleted();
    }

    [Test]
    public async Task CategoryExpenses_CheckDatabaseTestObjects_ShouldReturnSixObjectsInCategoryExpenses()
    {
      // Act
      var allCategoryExpenses = await queryDBInMemory.GetAllAsync();

      // Assert
      Assert.AreEqual(6, allCategoryExpenses.Count());
    }

    [Test]
    public async Task AddCategoryExpense_ShouldRunAddRangeAsyncOnlyOnce()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      await sut.AddCategoryExpense(categoryExpenseModelLists);

      // Assert
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()),
        Times.Once, "AddRangeAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldBeAbleToAddCategoryExpense()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(categoryExpenseModelLists);

      // Assert
      Assert.IsTrue(resultOfAddCategoryExpense, "Add and Save should return true.");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_TryingAddNewObjectToDatabase_ShouldBeAbleReturnIdEquals8()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      categoryExpenseEntityLists = new List<Entities.CategoryExpense>
      {
        new Entities.CategoryExpense { Id = 8, Description = "New category Expense was added" }
      };
      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(categoryExpenseModelLists);

      await context.CategoryExpenses.AddRangeAsync(categoryExpenseEntityLists);
      await context.SaveChangesAsync();
      var isAddedNewObject = queryDBInMemory.GetAsync(8);

      // Assert
      Assert.AreEqual(8, isAddedNewObject.Result.Id, "New object was not added, require id=8");
      Assert.IsTrue(resultOfAddCategoryExpense, "Add and Save should return true. Object i added to Database");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotBeAbleToAddCategoryExpense()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(categoryExpenseModelLists);

      // Assert
      Assert.IsFalse(resultOfAddCategoryExpense, "Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotBeAbleToAddRandeAsync()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return false; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(categoryExpenseModelLists);

      // Assert
      Assert.IsFalse(resultOfAddCategoryExpense, "Add and Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnFalseWhenGetAsyncReturnNull()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult((Entities.CategoryExpense) null));
      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, 1);

      // Assert
      Assert.IsFalse(resultOfEditCategoryExpense, "GetAsync should return null.");
      mockRepo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnFalseWhenSaveAsyncFailed()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryExpenseEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, 1);

      // Assert
      Assert.IsFalse(resultOfEditCategoryExpense, "SaveAsync failed when edit CategoryExpense.");
      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnTrueWhenSaveAsyncSuccessful()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryExpenseEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, 1);

      // Assert
      Assert.IsTrue(resultOfEditCategoryExpense, "SaveAsync should successful when edit CategoryExpense.");
      mockRepo.Verify(
        x => x.GetAsync(1), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task PutCategoryExpense_TryingChangeEditingObjectInDatabase_ShouldBeAbleEditObjectAndSaveChangesToDatabase()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryExpenseEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, null);
      categoryExpenseModelObj = new Models.CategoryExpense { Id = 2, Description = "x2 Changed" };

      var entityFromDB = queryDBInMemory.GetAsync(2);
      entityFromDB.Result.Description = categoryExpenseModelObj.Description;
      await context.SaveChangesAsync();
      var isUpdatedNewObject = queryDBInMemory.GetAsync(2);
      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, 2);

      // Assert
      Assert.IsTrue(resultOfEditCategoryExpense, "SaveAsync should successful when edit CategoryExpense.");
      Assert.AreEqual(2, isUpdatedNewObject.Result.Id, "Object was not updated, require id=2");
      Assert.AreEqual("x2 Changed", isUpdatedNewObject.Result.Description, "Object was not updated, require Description=x2 Changed");
      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }
  }

}
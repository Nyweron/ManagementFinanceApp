using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Repository.CategoryIncome;
using ManagementFinanceApp.Service.CategoryIncome;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Service
{
  [TestFixture]
  public class CategoryIncomeServiceTest
  {
    private Mock<ICategoryIncomeRepository> mockRepo;
    private Mock<IMapper> mockMapper;
    private Entities.CategoryIncome categoryIncomeEntityObj;
    private IEnumerable<Entities.CategoryIncome> categoryIncomeEntityLists;
    private Models.CategoryIncome categoryIncomeModelObj;
    private List<Models.CategoryIncome> categoryIncomeModelLists;
    private DbContextOptions<ManagementFinanceAppDbContext> options;
    private ManagementFinanceAppDbContext context;
    private Repository.Repository<Entities.CategoryIncome> queryDBInMemory;
    private async Task Seed(ManagementFinanceAppDbContext context)
    {
      var entityCategoryIncomes = new []
      {
        new Entities.CategoryIncome { Id = 1, Description = "x1" },
        new Entities.CategoryIncome { Id = 2, Description = "x2" },
        new Entities.CategoryIncome { Id = 3, Description = "x3" },
        new Entities.CategoryIncome { Id = 4, Description = "x4" },
        new Entities.CategoryIncome { Id = 5, Description = "x5" },
        new Entities.CategoryIncome { Id = 6, Description = "x6" }
      };

      await context.CategoryIncomes.AddRangeAsync(entityCategoryIncomes);
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
      queryDBInMemory = new Repository.Repository<Entities.CategoryIncome>(context);
    }

    [SetUp]
    public async Task Setup()
    {
      await InitDatabaseInMemoryTest();

      categoryIncomeModelLists = new List<Models.CategoryIncome>();
      categoryIncomeEntityLists = new List<Entities.CategoryIncome>();
      mockRepo = new Mock<ICategoryIncomeRepository>();
      mockMapper = new Mock<IMapper>();
      categoryIncomeEntityObj = new Entities.CategoryIncome { Id = 2, Description = "CategoryIncome1", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryIncomeModelObj = new Models.CategoryIncome { Id = 2, Description = "CategoryIncome2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryIncomeModelLists.Add(categoryIncomeModelObj);
    }

    [TearDown]
    public void Teardown()
    {
      context.Database.EnsureDeleted();
    }

    [Test]
    public async Task CategoryIncomeGetAll_TryingGetAllObjectsFromDB_ShouldReturnSixObjectsInCategoryIncomes()
    {
      // Act
      var allCategoryIncomes = await queryDBInMemory.GetAllAsync();

      // Assert
      Assert.AreEqual(6, allCategoryIncomes.Count(), "GetAllAsync doesn't return objects from Database");
    }

    [Test]
    public async Task CategoryIncomeGet_TryingGetObjectFromDB_ShouldReturnOneObjectInCategoryIncomes()
    {
      // Act
      var getCategoryIncome = await queryDBInMemory.GetAsync(1);

      // Assert
      Assert.AreEqual(1, getCategoryIncome.Id, "GetAsync doesn't return object from Database");
    }

    [Test]
    public async Task CategoryIncomeRemove_TryingRemoveObjectFromDB_ShouldRemoveOneObjectInCategoryIncomes()
    {
      // Arrange
      var getCategoryIncome = await queryDBInMemory.GetAsync(1);
      var allCategoryIncomes = await queryDBInMemory.GetAllAsync();

      // Act
      var isRemoved = await queryDBInMemory.RemoveAsync(getCategoryIncome);
      var allCategoryIncomesAfterRemoveOne = await queryDBInMemory.GetAllAsync();

      // Assert
      Assert.IsTrue(isRemoved, "RemoveAsync doesn't removed from DataBase");
      Assert.AreEqual(6, allCategoryIncomes.Count(), "GetAllAsync doesn't return objects from Database");
      Assert.AreEqual(5, allCategoryIncomesAfterRemoveOne.Count(), "GetAllAsync doesn't return objects from Database");
      Assert.AreEqual(1, getCategoryIncome.Id, "GetAsync doesn't return object from Database");
    }

    [Test]
    public async Task GetAllCategoryIncome_ShouldBeAbleToReturnTwoObjects()
    {
      // Arrange
      categoryIncomeEntityLists = new List<Entities.CategoryIncome>()
      {
        new Entities.CategoryIncome { Id = 8, Description = "description8" },
          new Entities.CategoryIncome { Id = 9, Description = "description9" }
      }.AsEnumerable();

      mockRepo.Setup(y => y.GetAllAsync())
        .Returns(Task.FromResult(categoryIncomeEntityLists));

      var sut = new CategoryIncomeService(mockRepo.Object, null);

      // Act
      var getAllEntities = await sut.GetAllAsync();

      // Assert
      Assert.AreEqual(2, getAllEntities.Count(), "GetAll doesn't return correct Count");
      mockRepo.Verify(x => x.GetAllAsync(), Times.Once, "GetAllAsync should run once");

    }

    [Test]
    public async Task GetCategoryIncome_ShouldBeAbleToReturnOneObjectWithIdEquals8()
    {
      // Arrange
      categoryIncomeEntityObj = new Entities.CategoryIncome { Id = 8, Description = "description8" };

      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryIncomeEntityObj));

      var sut = new CategoryIncomeService(mockRepo.Object, null);

      // Act
      var getAllEntities = await sut.GetAsync(8);

      // Assert
      Assert.AreEqual(8, getAllEntities.Id, "GetAsync doesn't return correct Object");
      mockRepo.Verify(x => x.GetAsync(8), Times.Once, "GetAsync should run once");
    }

    [Test]
    public async Task RemoveCategoryIncome_ShouldBeAbleToRemoveOneObjectWithIdEquals1()
    {
      // Arrange
      categoryIncomeEntityObj = new Entities.CategoryIncome { Id = 1, Description = "description1" };

      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryIncomeEntityObj));
      mockRepo.Setup(y => y.RemoveAsync(It.IsAny<Entities.CategoryIncome>()))
        .Returns(Task.FromResult(true));

      var sut = new CategoryIncomeService(mockRepo.Object, null);
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
    public async Task AddCategoryIncome_ShouldRunAddRangeAsyncOnlyOnce()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryIncome>>(It.IsAny<List<Models.CategoryIncome>>()))
        .Returns(It.IsAny<List<Entities.CategoryIncome>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()))
        .Returns(() => Task.Run(() => { })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, mockMapper.Object);

      // Act
      await sut.AddCategoryIncome(categoryIncomeModelLists);

      // Assert
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()),
        Times.Once, "AddRangeAsync should run once");
    }

    [Test]
    public async Task AddCategoryIncome_ShouldBeAbleToAddCategoryIncome()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryIncome>>(It.IsAny<List<Models.CategoryIncome>>()))
        .Returns(It.IsAny<List<Entities.CategoryIncome>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryIncome = await sut.AddCategoryIncome(categoryIncomeModelLists);

      // Assert
      Assert.IsTrue(resultOfAddCategoryIncome, "Add and Save should return true.");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task CategoryIncomeAdd_TryingAddNewObjectToDB_ShouldBeAbleReturnIdEquals8()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryIncome>>(It.IsAny<List<Models.CategoryIncome>>()))
        .Returns(It.IsAny<List<Entities.CategoryIncome>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      categoryIncomeEntityLists = new List<Entities.CategoryIncome>
      {
        new Entities.CategoryIncome { Id = 8, Description = "New category Expense was added" }
      };
      var sut = new CategoryIncomeService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryIncome = await sut.AddCategoryIncome(categoryIncomeModelLists);

      await context.CategoryIncomes.AddRangeAsync(categoryIncomeEntityLists);
      await context.SaveChangesAsync();
      var isAddedNewObject = queryDBInMemory.GetAsync(8);

      // Assert
      Assert.AreEqual(8, isAddedNewObject.Result.Id, "New object was not added, require id=8");
      Assert.IsTrue(resultOfAddCategoryIncome, "Add and Save should return true. Object i added to Database");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddCategoryIncome_ShouldNotBeAbleToAddCategoryIncome()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryIncome>>(It.IsAny<List<Models.CategoryIncome>>()))
        .Returns(It.IsAny<List<Entities.CategoryIncome>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryIncome = await sut.AddCategoryIncome(categoryIncomeModelLists);

      // Assert
      Assert.IsFalse(resultOfAddCategoryIncome, "Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task AddCategoryIncome_ShouldNotBeAbleToAddRandeAsync()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryIncome>>(It.IsAny<List<Models.CategoryIncome>>()))
        .Returns(It.IsAny<List<Entities.CategoryIncome>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()))
        .Returns(() => Task.Run(() => { return false; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, mockMapper.Object);

      // Act
      var resultOfAddCategoryIncome = await sut.AddCategoryIncome(categoryIncomeModelLists);

      // Assert
      Assert.IsFalse(resultOfAddCategoryIncome, "Add and Save should return false");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryIncome>>()),
        Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task PutCategoryIncome_ShouldBeAbleToReturnFalseWhenGetAsyncReturnNull()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult((Entities.CategoryIncome) null));
      var sut = new CategoryIncomeService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryIncome = await sut.EditCategoryIncome(categoryIncomeModelObj, 1);

      // Assert
      Assert.IsFalse(resultOfEditCategoryIncome, "GetAsync should return null.");
      mockRepo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
    }

    [Test]
    public async Task PutCategoryIncome_ShouldBeAbleToReturnFalseWhenSaveAsyncFailed()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryIncomeEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryIncome = await sut.EditCategoryIncome(categoryIncomeModelObj, 1);

      // Assert
      Assert.IsFalse(resultOfEditCategoryIncome, "SaveAsync failed when edit CategoryIncome.");
      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task PutCategoryIncome_ShouldBeAbleToReturnTrueWhenSaveAsyncSuccessful()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryIncomeEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryIncome = await sut.EditCategoryIncome(categoryIncomeModelObj, 1);

      // Assert
      Assert.IsTrue(resultOfEditCategoryIncome, "SaveAsync should successful when edit CategoryIncome.");
      mockRepo.Verify(
        x => x.GetAsync(1), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }

    [Test]
    public async Task CategoryIncomePut_TryingChangeEditingObjectInDB_ShouldBeAbleEditObjectAndSaveChangesToDatabase()
    {
      // Arrange
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryIncomeEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryIncomeService(mockRepo.Object, null);
      categoryIncomeModelObj = new Models.CategoryIncome { Id = 2, Description = "x2 Changed" };

      var entityFromDB = queryDBInMemory.GetAsync(2);
      entityFromDB.Result.Description = categoryIncomeModelObj.Description;
      await context.SaveChangesAsync();
      var isUpdatedNewObject = queryDBInMemory.GetAsync(2);
      // Act
      var resultOfEditCategoryIncome = await sut.EditCategoryIncome(categoryIncomeModelObj, 2);

      // Assert
      Assert.IsTrue(resultOfEditCategoryIncome, "SaveAsync should successful when edit CategoryIncome.");
      Assert.AreEqual(2, isUpdatedNewObject.Result.Id, "Object was not updated, require id=2");
      Assert.AreEqual("x2 Changed", isUpdatedNewObject.Result.Description, "Object was not updated, require Description=x2 Changed");
      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");
    }
  }

}
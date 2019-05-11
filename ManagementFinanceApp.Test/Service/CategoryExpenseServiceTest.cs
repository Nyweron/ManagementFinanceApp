using System.Collections.Generic;
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
    private Models.CategoryExpense categoryExpenseModelObj;

    [SetUp]
    public void Setup()
    {
      mockRepo = new Mock<ICategoryExpenseRepository>();
      mockMapper = new Mock<IMapper>();
      categoryExpenseEntityObj = new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense1", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelObj = new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
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
      await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldBeAbleToAddCategoryExpense()
    {
      // Arrange
      var expected = true;

      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      // Act
      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Add and Save should return true");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotBeAbleToAddCategoryExpense()
    {
      // Arrange
      var expected = false;

      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return true; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      // Act
      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Save should return false");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotBeAbleToAddRandeAsync()
    {
      // Arrange
      var expected = false;

      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { return false; })).Verifiable();
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      // Act
      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);
      var resultOfAddCategoryExpense = await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Add and Save should return false");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnFalseWhenGetAsyncReturnNull()
    {
      // Arrange
      var expected = false;
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult((Entities.CategoryExpense) null));
      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(It.IsAny<Models.CategoryExpense>(), It.IsAny<int>());

      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfEditCategoryExpense, "GetAsync should return null.");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnFalseWhenSaveAsyncFailed()
    {
      // Arrange
      var expected = false;
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryExpenseEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return false; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, It.IsAny<int>());

      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfEditCategoryExpense, "SaveAsync failed when edit CategoryExpense.");
    }

    [Test]
    public async Task PutCategoryExpense_ShouldBeAbleToReturnTrueWhenSaveAsyncSuccessful()
    {
      // Arrange
      var expected = true;
      mockRepo.Setup(y => y.GetAsync(It.IsAny<int>()))
        .Returns(Task.FromResult(categoryExpenseEntityObj));
      mockRepo.Setup(y => y.SaveAsync())
        .Returns(() => Task.Run(() => { return true; })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, null);

      // Act
      var resultOfEditCategoryExpense = await sut.EditCategoryExpense(categoryExpenseModelObj, It.IsAny<int>());

      mockRepo.Verify(
        x => x.GetAsync(It.IsAny<int>()), Times.Once, "GetAsync should run once");
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once, "SaveAsync should run once");

      // Assert
      Assert.AreEqual(expected, resultOfEditCategoryExpense, "SaveAsync should successful when edit CategoryExpense.");
    }

  }
}
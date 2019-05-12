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
    private List<Models.CategoryExpense> categoryExpenseModelLists;

    [SetUp]
    public void Setup()
    {
      categoryExpenseModelLists = new List<Models.CategoryExpense>();
      mockRepo = new Mock<ICategoryExpenseRepository>();
      mockMapper = new Mock<IMapper>();
      categoryExpenseEntityObj = new Entities.CategoryExpense { Id = 2, Description = "CategoryeExpense1", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelObj = new Models.CategoryExpense { Id = 2, Description = "CategoryeExpense2", IsDeleted = false, Weight = 2, CategoryGroupId = 2 };
      categoryExpenseModelLists.Add(categoryExpenseModelObj);
    }

    [Test]
    public async Task AddCategoryExpense_ShouldRunAddRangeAsyncOnlyOnce()
    {
      //entity framework testy dla in memory, skupić sie na biznesowej warstwie metody.

      // Arrange
      //It.IsAny<List<Models.CategoryExpense>>() - ustawiać podczas arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { })).Verifiable();

      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);

      // Act
      //W momencie ACT podawać zmienne z wartościoami lub elemeny z substituted (https://nsubstitute.github.io/)
      //W act powinna być tylko jedna linijka, jedna metoda, ktora jest testowana
      await sut.AddCategoryExpense(categoryExpenseModelLists);

      //schemat testowania wyjatkow, robie jednoczesnie ACT czyli await sut i assert
      // Assert.ThrowsAsync<ThreadStateException>(
      //   () => await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>())
      // );

      // Assert
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
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
      Assert.IsTrue(resultOfAddCategoryExpense, "Add and Save should return true");
      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
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
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
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
      mockRepo.Verify(x => x.GetAsync(1), Times.Once, "GetAsync should run once");
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

  }
}
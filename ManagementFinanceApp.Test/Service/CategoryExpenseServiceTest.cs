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

    [SetUp]
    public void Setup()
    {
      mockRepo = new Mock<ICategoryExpenseRepository>();
      mockMapper = new Mock<IMapper>();
    }

    [Test]
    public async Task AddCategoryExpense_ShouldRunAddRangeAsyncOnlyOnce()
    {
      // Arrange
      mockMapper.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(It.IsAny<List<Entities.CategoryExpense>>());
      mockRepo.Setup(y => y.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()))
        .Returns(() => Task.Run(() => { })).Verifiable();

      // Act
      var sut = new CategoryExpenseService(mockRepo.Object, mockMapper.Object);
      await sut.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once, "AddRangeAsync should run once");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldAddAndSaveObject()
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

      // Arrange
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Add and Save should return true");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotSaveObject()
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

      // Arrange
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Save should return false");
    }

    [Test]
    public async Task AddCategoryExpense_ShouldNotAddObject()
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

      // Arrange
      Assert.AreEqual(expected, resultOfAddCategoryExpense, "Add and Save should return false");
    }
  }
}
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
    IMapper mapper = AutoMapperConfig.GetMapper();

    [Test]
    public async Task AddCategoryExpense_ShouldRunAddRangeAsyncOnlyOnce()
    {
      // Arrange
      var mockRepo = new Mock<ICategoryExpenseRepository>();
      var mock = new Mock<IMapper>();

      var expectedCategoryExpensesList = new List<Entities.CategoryExpense>
      {
        new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 }
      };

      mock.Setup(x => x.Map<List<Entities.CategoryExpense>>(
        It.IsAny<List<Models.CategoryExpense>>())).Returns(expectedCategoryExpensesList);
      mockRepo.Setup(y => y.AddRangeAsync(
        It.IsAny<IEnumerable<Entities.CategoryExpense>>())).Returns(() => Task.Run(() => { })).Verifiable();

      // Act
      var categoryExpenseService = new CategoryExpenseService(mockRepo.Object, mapper);
      await categoryExpenseService.AddCategoryExpense(It.IsAny<List<Models.CategoryExpense>>());

      mockRepo.Verify(
        x => x.AddRangeAsync(It.IsAny<IEnumerable<Entities.CategoryExpense>>()), Times.Once);
      mockRepo.Verify(
        x => x.SaveAsync(), Times.Once);

    }
  }
}
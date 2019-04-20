using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.CategoryExpense;
using ManagementFinanceApp.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ManagementFinanceApp.Test.Service
{
  [TestFixture]
  public class CategoryExpenseServiceTest
  {
    IMapper mapper = AutoMapperConfig.GetMapper();

    [Test]
    public async Task AddCategoryExpense_ShouldReturnTrue()
    {
      // Arrange
      var expectedCategoryExpensesList = new List<Entities.CategoryExpense>();
      var expected = true;

      // Act
      var mock = new Mock<IMapper>();
      var mockRepo = new Mock<ICategoryExpenseRepository>();
      mock.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>())).Returns(expectedCategoryExpensesList);
      mockRepo.Setup(y => y.AddRangeAsync(expectedCategoryExpensesList)).Returns(Task.FromResult(true));
      mockRepo.Setup(y => y.SaveAsync()).Returns(Task.FromResult(true));
      //mock.Setup(x => x.AddAsync())

      //var controller = new CategoryExpenseController(null, null);
      // var badRequestResult = await controller.Post(categoryExpenseList) as BadRequestResult;

      // Assert
      Assert.AreEqual(mockRepo, true);
      // Assert.AreEqual(400, badRequestResult.StatusCode, "Badrequest does not works. Method post");
    }
  }
}
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
      /*
      https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
      https://docs.microsoft.com/en-us/ef/ef6/fundamentals/testing/mocking
      */

      // Arrange
      var expectedCategoryExpensesList = new List<Entities.CategoryExpense>
      {
        new Entities.CategoryExpense { Id = 1, Description = "CategoryeExpense1", IsDeleted = false, Weight = 1, CategoryGroupId = 1 }
      );
      var expected = true;
      var mock = new Mock<IMapper>();
      var mockRepo = new Mock<ICategoryExpenseRepository>();

      // Act
      mock.Setup(x => x.Map<List<Entities.CategoryExpense>>(It.IsAny<List<Models.CategoryExpense>>()))
        .Returns(expectedCategoryExpensesList);
      mockRepo.Setup(y => y.AddRangeAsync(expectedCategoryExpensesList)).Returns(Task.FromResult(expectedCategoryExpensesList));
      mockRepo.Setup(y => y.SaveAsync()).Returns(() => Task.Run(() => { return expected; })).Verifiable();

      // Assert
      //Assert.AreEqual()
      Assert.IsFalse(true); //fake assert... TODO fix it!

    }
  }
}
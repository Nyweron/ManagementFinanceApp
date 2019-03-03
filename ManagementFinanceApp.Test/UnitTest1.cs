using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;

namespace ManagementFinanceApp.Test
{
  [TestFixture]
  public class UnitTest1
  {
    [Test]
    public void ReturnFalseGivenValueOf1()
    {
      var result = false;

      Assert.IsFalse(result, "should be result equal false");
    }
  }
}
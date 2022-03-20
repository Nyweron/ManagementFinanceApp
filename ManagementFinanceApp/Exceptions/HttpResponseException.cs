using System;

namespace ManagementFinanceApp.Exceptions
{
  public class HttpResponseException : Exception
  {
    public int Status { get; set; } = 500;

    public object Value { get; set; }
  }
}

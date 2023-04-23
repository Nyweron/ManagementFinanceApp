using System;

namespace ManagementFinanceApp.Exceptions
{
  public class BadRequestException : Exception
  {
        public BadRequestException(string message): base(message)
        {
            
        }
    }
}

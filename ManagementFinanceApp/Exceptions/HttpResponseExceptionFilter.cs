using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManagementFinanceApp.Exceptions
{
  public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
  {
    public int Order { get; } = int.MaxValue - 10;

    // Wejdzie przed wejściem do kontrollera. Najpier tutaj potem kontroller
    public void OnActionExecuting(ActionExecutingContext context) 
    {
      var x = context;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (context.Exception is HttpResponseException exception)
      {
        context.Result = new ObjectResult(exception.Value)
        {
          StatusCode = exception.Status,
        };
        context.ExceptionHandled = true;
      }
    }
  }
}

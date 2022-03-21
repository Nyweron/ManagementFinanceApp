using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Middleware
{
  // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
  public class MyMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory)
    {
      _next = next;

      _logger = logFactory.CreateLogger("MyMiddleware");
    }

    public async Task Invoke(HttpContext httpContext)
    {
      _logger.LogInformation("MyMiddleware executing..");

      await _next(httpContext); // calling next middleware

    }
  }

  // Extension method used to add the middleware to the HTTP request pipeline.
  public static class MyMiddlewareExtensions
  {
    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    {
      return builder.UseMiddleware<MyMiddleware>();
    }
  }
}

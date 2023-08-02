using ManagementFinanceApp.Exceptions;
using ManagementFinanceApp.Service.UserContextService;
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
    private readonly IUserContextService _userContextService;

    public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory, IUserContextService userContextService)
    {
      _next = next;
      _userContextService = userContextService;

      _logger = logFactory.CreateLogger("MyMiddleware");
    }

    public async Task Invoke(HttpContext httpContext)
    {
      var user = _userContextService.User;
      var isLogin = _userContextService.IsLogin;
      _logger.LogInformation("MyMiddleware executing..");
      try
      {
        await _next(httpContext); // calling next middleware
      }
      catch(BadRequestException badRequestException)
      {
        httpContext.Response.StatusCode = 400;
        await httpContext.Response.WriteAsync(badRequestException.Message);
      }
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

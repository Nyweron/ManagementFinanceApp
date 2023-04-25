using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Authorization
{
  public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
  {

    private readonly ILogger<MinimumAgeRequirementHandler> _logger;

    public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
    {
      _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
      var weight = context.User.FindFirst(c => c.Type == "Weight").Value;

      var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name).Value;

      _logger.LogInformation($"User: {userEmail} with weight: {weight} ");

      if (requirement.MinimumAge < int.Parse(weight))
      {
        _logger.LogInformation("Authorization succedded");
        context.Succeed(requirement);
      } else
      {
        _logger.LogInformation("Authorization failed");
      }

      return Task.CompletedTask;
    }
  }
}

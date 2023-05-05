using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Authorization
{
  public class MinimumWeighRequirementHandler : AuthorizationHandler<MinimumWeightRequirement>
  {

    private readonly ILogger<MinimumWeighRequirementHandler> _logger;

    public MinimumWeighRequirementHandler(ILogger<MinimumWeighRequirementHandler> logger)
    {
      _logger = logger;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumWeightRequirement requirement)
    {
      var weight = context.User.FindFirst(c => c.Type == "Weight")?.Value;

      var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;

      _logger.LogInformation($"User: {userEmail} with weight: {weight} ");

      if (!weight.IsNullOrEmpty() && requirement.MinimumAge < int.Parse(weight))
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

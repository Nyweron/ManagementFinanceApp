using Microsoft.AspNetCore.Authorization;

namespace ManagementFinanceApp.Authorization
{
  public class MinimumWeightRequirement : IAuthorizationRequirement
  {
    public int MinimumAge { get; }

    public MinimumWeightRequirement(int minimumAge)
    {
      MinimumAge = minimumAge;
    }


  }

}

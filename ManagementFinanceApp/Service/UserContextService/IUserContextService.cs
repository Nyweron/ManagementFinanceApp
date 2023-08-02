using System.Security.Claims;

namespace ManagementFinanceApp.Service.UserContextService
{
  public interface IUserContextService
  {
    int? GetUserId { get; }
    bool IsLogin { get; }
    ClaimsPrincipal User { get; }
  }
}
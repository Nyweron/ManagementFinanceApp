using System.Security.Claims;

namespace ManagementFinanceApp.Service.UserContextService
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}
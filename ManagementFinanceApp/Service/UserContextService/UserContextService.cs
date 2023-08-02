using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ManagementFinanceApp.Service.UserContextService
{
  public class UserContextService : IUserContextService
  {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal User
    {
      get
      {
        var x = _httpContextAccessor.HttpContext.User;
        return x;
      }
    }

    //TODO wez id lub islogin za kazdym strzalem do backendu z claimsow, middleware?
    //TODO logout remove z tabeli userRefreshToken
    //TODO akcja z logout, clear token na backendzie clean claims
    //TODO dlaczego w middlewarze isLogin jest false a po wejscie w expenseController jest true?
    public int? GetUserId
    {
      get
      {
        int? x = User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        return x;
      }
    }

    public bool IsLogin
    {
      get
      {
        var isLogin = User is null ? "" : (User.FindFirst(c => c.Type.Contains("IsLogin"))?.Value);
        return isLogin.IsNullOrEmpty() ? false : true;
      }
    }
  }
}

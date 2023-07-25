using ManagementFinanceApp.Models;
using System.Security.Claims;

namespace ManagementFinanceApp.Service.Account
{
  public interface IAccountService
  {
    Tokens GenerateJwtWhenLogin(LoginDto dto);
    void RegisterUser(RegisterUserDto dto);
    Tokens GenerateRefreshToken(string userName);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
  }
}

using ManagementFinanceApp.Models;

namespace ManagementFinanceApp.Service.Account
{
  public interface IAccountService
  {
    string GenerateJwt(LoginDto dto);
    void RegisterUser(RegisterUserDto dto);
  }
}

using ManagementFinanceApp.Models;

namespace ManagementFinanceApp.Service.Account
{
  public interface IAccountService
  {
    void RegisterUser(RegisterUserDto dto);
  }
}

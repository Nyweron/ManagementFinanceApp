using ManagementFinanceApp.Entities;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.UserRefreshTokenRepo
{
  public interface IUserRefreshTokenRepository
  {
    Task<bool> IsValidUserAsync(Entities.User users);

    UserRefreshToken AddUserRefreshTokens(UserRefreshToken user);

    UserRefreshToken GetSavedRefreshTokens(string username, string refreshToken);

    void DeleteUserRefreshTokens(string email, string refreshToken);

    int SaveCommit();
  }
}

using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Repository.UserRefreshTokenRepo
{
  public class UserRefreshTokenRepository : Repository<Entities.UserRefreshToken>, IUserRefreshTokenRepository
  {

    public UserRefreshTokenRepository(ManagementFinanceAppDbContext context) : base(context) { }

    public ManagementFinanceAppDbContext ManagementFinanceAppDbContext
    {
      get { return Context as ManagementFinanceAppDbContext; }
    }

    public UserRefreshToken AddUserRefreshTokens(UserRefreshToken user)
    {
      ManagementFinanceAppDbContext.UserRefreshTokens.Add(user);
      return user;
    }

    public void DeleteUserRefreshTokens(string email, string refreshToken)
    {
      var item = ManagementFinanceAppDbContext.UserRefreshTokens.FirstOrDefault(x => x.UserName == email && x.RefreshToken == refreshToken);
      if (item != null)
      {
        ManagementFinanceAppDbContext.UserRefreshTokens.Remove(item);
      }
    }

    public UserRefreshToken GetSavedRefreshTokens(string email, string refreshToken)
    {
      return ManagementFinanceAppDbContext
        .UserRefreshTokens
        .FirstOrDefault(x => x.UserName == email 
                          && x.RefreshToken == refreshToken 
                          && x.IsActive);
    }

    public int SaveCommit()
    {
      return ManagementFinanceAppDbContext.SaveChanges();
    }

    public async Task<bool> IsValidUserAsync(Entities.User users)
    {
      var u = await ManagementFinanceAppDbContext.Users.FirstOrDefaultAsync(o => o.Email == users.Email);
      if (u != null && u.PasswordHash != null && u.PasswordHash == users.PasswordHash)
      {
        return true;
      }

      return false;
    }
  }
}

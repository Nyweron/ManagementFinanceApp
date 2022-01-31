using ManagementFinanceApp.Models;
using ManagementFinanceApp.Service.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManagementFinanceApp.Adapter
{
  public class UserAdapter : IUserAdapter
  {

    private IUserService _userService;

    public UserAdapter(IUserService userService)
    {
      _userService = userService;
    }

    public async Task<IEnumerable<ViewDataForSelect>> GetUserList()
    {
      var users = await _userService.GetAllAsync();
      var usersViewDataForSelect = new List<ViewDataForSelect>();

      foreach (var user in users)
      {
        if (user == null)
        {
          continue;
        }

        usersViewDataForSelect.Add(new ViewDataForSelect
        {
          text = user.FirstName + " " + user.LastName,
          value = user.Id.ToString(),
        });
      }

      return usersViewDataForSelect;
    }
  }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.User;

namespace ManagementFinanceApp.Service.User
{
  public class UserService : IUserService
  {
    private IUserRepository _userRepository;
    private IMapper _mapper;
    public UserService(IUserRepository userRepository,
      IMapper mapper
    )
    {
      _userRepository = userRepository;
      _mapper = mapper;
    }

    public async Task<bool> AddUser(Models.UserDto user)
    {
      var userEntity = _mapper.Map<Entities.User>(user);
      await _userRepository.AddAsync(userEntity);

      if (!await _userRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<bool> EditUser(Models.UserDto user, int userId)
    {
      var existedUser = await _userRepository.GetAsync(userId);
      var mappedUser = _mapper.Map<Entities.User>(user);

      if (existedUser.Address != user.Address && user.Address != null) { existedUser.Address = user.Address; }
      if (existedUser.Email != user.Email && user.Email != null) { existedUser.Email = user.Email; }
      if (existedUser.FirstName != user.FirstName && user.FirstName != null) { existedUser.FirstName = user.FirstName; }
      if (existedUser.LastName != user.LastName && user.LastName != null) { existedUser.LastName = user.LastName; }
      if (existedUser.Nick != user.Nick && user.Nick != null) { existedUser.Nick = user.Nick; }
      if (existedUser.Phone != user.Phone && user.Phone != null) { existedUser.Phone = user.Phone; }

      //Idea - Front should send All data about exist and changed data to backend then backend only update row in db?

      await _userRepository.UpdateUserAsync(existedUser);

      if (!await _userRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }

    public async Task<IEnumerable<Entities.User>> GetAllAsync()
    {
      var x = await _userRepository.GetAllAsync();
      var orderByIds = x.OrderBy(o => o.Id).ToList();
      return orderByIds;
    }
  }
}
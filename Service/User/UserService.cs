using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Repository.User;
using Microsoft.AspNetCore.Mvc;

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

      //TO DO write code which create data entity + model = objtToSaveWithNewDataFromModelAndRestDataFromExistEntity

      var x = 0;

      await _userRepository.UpdateUserAsync(mappedUser);

      if (!await _userRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return false;
      }

      return true;
    }
  }
}
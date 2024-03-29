using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementFinanceApp.Adapter;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.User;
using ManagementFinanceApp.Service.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private IUserRepository _userRepository;
    private IUserService _userService;
    private IUserAdapter _userAdapter;

    public UserController(IUserRepository userRepository, IUserService userService, IUserAdapter userAdapter)
    {
      _userRepository = userRepository;
      _userService = userService;
      _userAdapter = userAdapter;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var userEntities = await _userRepository.GetAllAsync();
      return Ok(userEntities);
    }

    [HttpGet("GetUsersForSelect")]
    public async Task<IActionResult> GetUsersForSelect()
    {
      var userViewList = await _userAdapter.GetUserList();
      return Ok(userViewList);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> Get(int userId)
    {
      try
      {
        if (!await _userRepository.UserExistsAsync(userId))
        {
          // _logger.LogInformation($"User with id {userId} wasn't found when accessing to UserController/Get(int userId).");
          return NotFound();
        }

        var userEntities = await _userRepository.GetAsync(userId);
        return Ok(userEntities);
      }
      catch (Exception)
      {
        // _logger.LogCritical($"Exception {userId}.", ex);
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserDto user)
    {
      if (user == null)
      {
        //_logger.LogInformation($"User is empty when accessing to UserController/Post(UserDto user).");
        return BadRequest();
      }

      if (await _userRepository.EmailExistsAsync(user.Email))
      {
        // _logger.LogInformation($"The Email {user.Email} exist in database, use other email. UserController/Post(UserDto user).");
        return BadRequest($"The Email {user.Email} exist, user other email.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isCreated = await _userService.AddUser(user);

      if (isCreated)
      {
        return Created("", null);
      }
      else
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Put([FromBody] UserDto user, int userId)
    {
      if (user == null)
      {
        return BadRequest();
      }

      if (!await _userRepository.UserExistsAsync(userId))
      {
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var isUpdated = await _userService.EditUser(user, userId);
      if (isUpdated)
      {
        //TODO: Implement Realistic Implementation
        return Ok();
      }
      else
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      if (!await _userRepository.UserExistsAsync(id))
      {
        // _logger.LogInformation($"User with id: {id} is not exist. When accessing to UserController/Delete(int id).");
        return NotFound();
      }

      var user = await _userRepository.GetAsync(id);

      if (!await _userRepository.RemoveAsync(user))
      {
        //_logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }

  }
}
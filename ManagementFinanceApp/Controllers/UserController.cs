using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {

    private IUserRepository _userRepository;
    private IMapper _mapper;
    public UserController(IUserRepository userRepository,
      IMapper mapper
    )
    {
      _userRepository = userRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var userEntities = await _userRepository.GetAllAsync();
      return Ok(userEntities);
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

      var userEntity = _mapper.Map<User>(user);
      await _userRepository.AddAsync(userEntity);

      if (!await _userRepository.SaveAsync())
      {
        // _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
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

      var updatedUser = _mapper.Map<User>(user);
      updatedUser.Id = userId;
      await _userRepository.UpdateUserAsync(updatedUser);

      if (!await _userRepository.SaveAsync())
      {
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
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
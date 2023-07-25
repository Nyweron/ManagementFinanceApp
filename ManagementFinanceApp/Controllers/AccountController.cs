using ManagementFinanceApp.Entities;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.UserRefreshTokenRepo;
using ManagementFinanceApp.Service.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementFinanceApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService,
                             IUserRefreshTokenRepository userRefreshTokenRepository)
    {
      _accountService = accountService;
      _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
    {
      _accountService.RegisterUser(dto);

      return Ok();
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto dto)
    {
      var token = _accountService.GenerateJwtWhenLogin(dto);


      ///////////////////////////////////////////////
      // saving refresh token to the db
      UserRefreshToken obj = new UserRefreshToken
      {
        RefreshToken = token.RefreshToken,
        UserName = dto.Email
      };

      _userRefreshTokenRepository.DeleteUserRefreshTokens(dto.Email, token.RefreshToken);
      _userRefreshTokenRepository.AddUserRefreshTokens(obj);
      _userRefreshTokenRepository.SaveCommit();
      /////////////////////////////////////////////////


      return Ok(token);
    }

    //[AllowAnonymous]
    //[HttpPost]
    //[Route("authenticate")]
    //public async Task<IActionResult> AuthenticateAsync(Entities.User usersdata)
    //{
    //  var validUser = await _userRefreshTokenRepository.IsValidUserAsync(usersdata);

    //  if (!validUser)
    //  {
    //    return Unauthorized("Incorrect username or password!");
    //  }

    //  var token = _accountService.GenerateToken(usersdata.Email);

    //  if (token == null)
    //  {
    //    return Unauthorized("Invalid Attempt!");
    //  }

    //  // saving refresh token to the db
    //  UserRefreshToken obj = new UserRefreshToken
    //  {
    //    RefreshToken = token.Refresh_Token,
    //    UserName = usersdata.Email
    //  };

    //  _userRefreshTokenRepository.AddUserRefreshTokens(obj);
    //  _userRefreshTokenRepository.SaveCommit();
    //  return Ok(token);
    //}

    [AllowAnonymous]
    [HttpPost]
    [Route("refresh")]
    public IActionResult Refresh(Tokens token)
    {
      var principal = _accountService.GetPrincipalFromExpiredToken(token.Token);
      var username = principal.Identity?.Name;

      //retrieve the saved refresh token from database
      var savedRefreshToken = _userRefreshTokenRepository.GetSavedRefreshTokens(username, token.RefreshToken);

      if (savedRefreshToken?.RefreshToken != token.RefreshToken)
      {
        return Unauthorized("Invalid attempt!");
      }

      var newJwtToken = _accountService.GenerateRefreshToken(username);

      if (newJwtToken == null)
      {
        return Unauthorized("Invalid attempt!");
      }

      // saving refresh token to the db
      UserRefreshToken obj = new UserRefreshToken
      {
        RefreshToken = newJwtToken.RefreshToken,
        UserName = username
      };

      _userRefreshTokenRepository.DeleteUserRefreshTokens(username, token.RefreshToken);
      _userRefreshTokenRepository.AddUserRefreshTokens(obj);  
      _userRefreshTokenRepository.SaveCommit();

      return Ok(newJwtToken);
    }
  }
}

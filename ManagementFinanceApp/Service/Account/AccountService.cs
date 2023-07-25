using AutoMapper;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Exceptions;
using ManagementFinanceApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ManagementFinanceApp.Service.Account
{
  public class AccountService : IAccountService
  {
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<RegisterUserDto> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly ManagementFinanceAppDbContext _context;

    public AccountService(IMapper mapper, IPasswordHasher<RegisterUserDto> passwordHasher, AuthenticationSettings authenticationSettings, ManagementFinanceAppDbContext context)
    {
      _mapper = mapper;
      _passwordHasher = passwordHasher;
      _authenticationSettings = authenticationSettings;
      _context = context;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
      var newUser = new RegisterUserDto
      {
        Email = dto.Email,
        FirstName = dto.FirstName,
        Nick = dto.Nick,
        RoleId = dto.RoleId,
      };

      var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
      newUser.Password = hashedPassword;

      var userEntity = _mapper.Map<Entities.User>(newUser);
      _context.Users.Add(userEntity);
      _context.SaveChanges();
    }

    public Tokens GenerateJwtWhenLogin(LoginDto dto)
    {
      var user = _context.Users.Include(r => r.Role).FirstOrDefault(u => u.Email == dto.Email);

      if (user is null)
      {
        throw new BadRequestException("Invalid username or passowrd");
      }

      var registerUserDto = _mapper.Map<RegisterUserDto>(user);

      var result = _passwordHasher.VerifyHashedPassword(registerUserDto, registerUserDto.Password, dto.Password);

      if (result == PasswordVerificationResult.Failed)
      {
        throw new BadRequestException("Invalid username or passowrd");
      }

      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, $"{user.Email}"),
        new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
        new Claim("RoleId", $"{user.RoleId}"),
        new Claim("Weight", $"{user.Weight}"),
        new Claim("IsLogin", $"{true}"),
        new Claim(ClaimTypes.Email, $"{user.Email}")
      };

      if (!string.IsNullOrEmpty(user.Nick))
      {
        claims.Add(
          new Claim("Nick", user.Nick)
          );
      }

      return GenerateJwtToken(claims);
    }

    public Tokens GenerateRefreshToken(string username)
    {
      return GenerateJwtWhenRefreshTokens(username);
    }

    public Tokens GenerateJwtWhenRefreshTokens(string userName)
    {
      try
      {
        var claims = new List<Claim>
        {
          new Claim(ClaimTypes.Name, $"{userName}"),
        };

        return GenerateJwtToken(claims);
      }
      catch (Exception ex)
      {
        return null;
      }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
      var Key = Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey);

      var tokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key),
        ClockSkew = TimeSpan.Zero
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
      JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
      if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
      {
        throw new SecurityTokenException("Invalid token");
      }


      return principal;
    }

    private Tokens GenerateJwtToken(List<Claim> claims)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
      var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddSeconds(60);
     // var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

      var token = new JwtSecurityToken(
        _authenticationSettings.JwtIssuer,
        _authenticationSettings.JwtIssuer,
        claims,
        expires: expires,
        signingCredentials: cred
      );
      var refreshToken = GenerateRefreshToken();

      var tokenHandler = new JwtSecurityTokenHandler();
      var tokenResult = tokenHandler.WriteToken(token);

      return new Tokens
      {
        Token = tokenResult,
        RefreshToken = refreshToken,
      };
    }

    private string GenerateRefreshToken()
    {
      var randomNumber = new byte[32];
      using (var rng = RandomNumberGenerator.Create())
      {
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
      }
    }

  }
}

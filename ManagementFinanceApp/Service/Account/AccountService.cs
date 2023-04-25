using AutoMapper;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Exceptions;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

    public string GenerateJwt(LoginDto dto)
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
        new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
        new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
        new Claim("Weight", $"{user.Weight}")
    };

      if (!string.IsNullOrEmpty(user.Nick))
      {
        claims.Add(
          new Claim("Nick", user.Nick)
          );
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
      var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var expires = DateTime.Now.AddMinutes(_authenticationSettings.JwtExpireMinutes);

      var token = new JwtSecurityToken(
        _authenticationSettings.JwtIssuer,
        _authenticationSettings.JwtIssuer,
        claims,
        expires: expires,
        signingCredentials: cred
      );

      var tokenHandler = new JwtSecurityTokenHandler();
      return tokenHandler.WriteToken(token);
    }
  }
}

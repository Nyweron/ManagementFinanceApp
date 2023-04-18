using AutoMapper;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.Account;
using Microsoft.AspNetCore.Identity;

namespace ManagementFinanceApp.Service.Account
{
  public class AccountService : IAccountService
  {
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<RegisterUserDto> _passwordHasher;

    public AccountService(IAccountRepository accountRepository, IMapper mapper, IPasswordHasher<RegisterUserDto> passwordHasher)
    {
      _accountRepository = accountRepository;
      _mapper = mapper;
      _passwordHasher = passwordHasher;
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
      _accountRepository.Add(userEntity);
      _accountRepository.SaveAsync();
    }
  }
}

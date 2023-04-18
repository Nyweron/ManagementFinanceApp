using AutoMapper;
using ManagementFinanceApp.Models;
using ManagementFinanceApp.Repository.Account;

namespace ManagementFinanceApp.Service.Account
{
  public class AccountService : IAccountService
  {
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;


    public AccountService(IAccountRepository accountRepository, IMapper mapper)
    {
      _accountRepository = accountRepository;
      _mapper = mapper;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
      var newUser = new UserDto
      {
        Email = dto.Email,
        FirstName = dto.FirstName,
        Nick = dto.Nick,
        RoleId = dto.RoleId,
      };

      var userEntity = _mapper.Map<Entities.User>(newUser);
      _accountRepository.Add(userEntity);
      _accountRepository.SaveAsync();
    }
  }
}

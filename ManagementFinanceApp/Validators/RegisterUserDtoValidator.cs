using FluentValidation;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Models;
using System.Linq;

namespace ManagementFinanceApp.Validators
{
  public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
  {
    public RegisterUserDtoValidator(ManagementFinanceAppDbContext dbContext)
    {
      RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

      RuleFor(x => x.Password).MinimumLength(6);

      RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

      RuleFor(x => x.Email)
        .Custom((value, context) =>
        {
          var emailInUse = dbContext.Users.Any(u=>u.Email == value);

          if(emailInUse)
          {
            context.AddFailure("Email", "That email is taken");
          }
        });
    }
  }
}

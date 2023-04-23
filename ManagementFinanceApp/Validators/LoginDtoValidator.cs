using FluentValidation;
using ManagementFinanceApp.Data;
using ManagementFinanceApp.Models;
using System.Linq;

namespace ManagementFinanceApp.Validators
{
  public class LoginDtoValidator : AbstractValidator<LoginDto>
  {
    public LoginDtoValidator(ManagementFinanceAppDbContext dbContext)
    {
      RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

      RuleFor(x => x.Password).MinimumLength(6);

      RuleFor(x => x.Email)
      .Custom((value, context) =>
      {
        var emailInUse = dbContext.Users.Any(u => u.Email == value);

        if (!emailInUse)
        {
          context.AddFailure("Email or Password", "Email or password are incorrect");
        }
      });
    }
  }
}

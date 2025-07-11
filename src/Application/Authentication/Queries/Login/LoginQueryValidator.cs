using FluentValidation;

namespace Application.Authentication.Queries.Login;


public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
   public LoginQueryValidator()
   {
      RuleFor(x => x.Email).NotEmpty().NotNull();
      RuleFor(x => x.Password).NotEmpty().NotNull();
   }
}
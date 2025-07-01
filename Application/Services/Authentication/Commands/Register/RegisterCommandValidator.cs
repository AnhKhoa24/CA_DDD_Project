using FluentValidation;

namespace Application.Services.Authentication.Commands.Register;


public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().NotNull();
        RuleFor(x => x.LastName).NotEmpty().NotNull();
        RuleFor(x => x.Email).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotEmpty().NotNull();
    }
}


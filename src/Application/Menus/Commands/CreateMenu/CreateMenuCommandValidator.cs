using FluentValidation;

namespace Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
   public CreateMenuCommandValidator()
   {
      RuleFor(x => x.Name)
         .NotEmpty()
         .WithName("Tên món ăn")
         .WithMessage("{PropertyName} không được bỏ trống.");
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Sections).NotEmpty();
   }
}
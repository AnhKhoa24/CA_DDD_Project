using FluentValidation;

namespace Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
   public CreateMenuCommandValidator()
   {
      RuleFor(x => x.Name)
         .NotEmpty();
      // .WithName("Tên món ăn")
      // .WithMessage("{PropertyName} không được bỏ trống.");
      RuleFor(x => x.Description);

      RuleFor(x => x.Sections).NotEmpty();

      RuleForEach(x => x.Sections).ChildRules(section =>
        {
           section.RuleFor(s => s.Name)
               .NotEmpty();

           section.RuleFor(s => s.Description)
               .NotEmpty();

           section.RuleFor(s => s.Items)
               .NotEmpty();

            section.RuleForEach(s => s.Items).ChildRules(item =>
            {
               item.RuleFor(i => i.Name)
                   .NotEmpty();

               item.RuleFor(i => i.Description)
                   .NotEmpty();
            });
        });
   }
}
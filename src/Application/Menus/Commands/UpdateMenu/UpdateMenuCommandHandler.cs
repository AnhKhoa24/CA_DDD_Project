using Application.Common.Interfaces.Persistence;
using Application.Menus.Common;
using Domain.Common.Errors;
using Domain.Menu;
using Domain.Menu.Entities;
using ErrorOr;
using MediatR;

namespace Application.Menus.Commands.UpdateMenu;

public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, ErrorOr<Menu>>
{
   private readonly IMenuRepository _menuRepository;

   public UpdateMenuCommandHandler(IMenuRepository menuRepository)
   {
      _menuRepository = menuRepository;
   }

   public async Task<ErrorOr<Menu>> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
   {
      var menu = await _menuRepository.GetMenuByIdAsync(menuId: request.Id.StringToMenuId());

      if (menu is not Menu)
      {
         return Errors.Menu.MenuNotExists;
      }
      var updateSections = ConvertToMenuSection(request.Sections);
      menu
         .WithName(request.Name)
         .WithDescription(request.Description)
         .UpdateSections(updateSections);
  
      await _menuRepository.SaveChangesAsync();

      return menu;
   }

   private static List<MenuSection> ConvertToMenuSection(List<UpdateMenuSectionCommand> sectionCommand)
   {
      return sectionCommand
         .ConvertAll(
            s => MenuSection.CreateWithId(
               s.Id.StringToMenuSectionId(),
               s.Name,
               s.Description,
               s.Items.ConvertAll(item => MenuItem.Create(
               item.Name,
               item.Description))
            )
         );
   }
  
}

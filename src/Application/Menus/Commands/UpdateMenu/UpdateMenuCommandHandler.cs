using Application.Common.Interfaces.Persistence;
using Domain.Common.Errors;
using Domain.Common.Models;
using Domain.Menu;
using Domain.Menu.Entities;
using Domain.Menu.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http.Features;

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
      var menu = await _menuRepository.GetMenuByIdAsync(menuId: MenuId.Create(Guid.Parse(request.Id)));

      if (menu is not Menu)
      {
         return Errors.Menu.MenuNotExists;
      }
      var sections = ConvertToMenuSection(request.Sections);
      menu.UpdateSections(sections);
  
      await _menuRepository.SaveChangesAsync();

      return menu;
   }

   private List<MenuSection> ConvertToMenuSection(List<UpdateMenuSectionCommand> sectionCommand)
   {
      return sectionCommand
         .ConvertAll(
            s => MenuSection.CreateWithId(
               ConvertMenuSectionId(s.Id),
               s.Name,
               s.Description,
               s.Items.ConvertAll(item => MenuItem.Create(
               item.Name,
               item.Description))
            )
         );
   }
   private MenuSectionId ConvertMenuSectionId(string? input)
   {
      return Guid.TryParse(input, out var guid) ? MenuSectionId.Create(guid) : MenuSectionId.Create(Guid.Empty);;
   }
   private MenuItemId ConvertMenuItemId(string? input)
   {
      return Guid.TryParse(input, out var guid) ? MenuItemId.Create(guid) : MenuItemId.Create(Guid.Empty);
   }
}

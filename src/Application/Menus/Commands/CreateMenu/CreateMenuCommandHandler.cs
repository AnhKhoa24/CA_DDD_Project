using Application.Common.Cache.Interfaces;
using Application.Common.Interfaces.Persistence;
using Application.Menus.Cache;
using Domain.Menu;
using Domain.Menu.Entities;
using ErrorOr;
using MediatR;

namespace Application.Menus;


public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
   private readonly IMenuRepository _menuRepository;
   public CreateMenuCommandHandler(IMenuRepository menuRepository)
   {
      _menuRepository = menuRepository;
   }

   public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
   {
      var menu = Menu.Create(
         name: request.Name,
         description: request.Description,
         sections: request.Sections.ConvertAll(section => MenuSection.Create(
            section.Name,
            section.Description,
            section.Items.ConvertAll(item => MenuItem.Create(
               item.Name,
               item.Description))
         ))
      );

      await _menuRepository.AddMenuAsync(menu);

      return menu;
   }
}

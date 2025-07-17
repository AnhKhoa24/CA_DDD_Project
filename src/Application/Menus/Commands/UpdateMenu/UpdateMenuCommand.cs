using Domain.Menu;
using ErrorOr;
using MediatR;

namespace Application.Menus.Commands.UpdateMenu;

public record UpdateMenuCommand(
   string Id,
   string Name,
   string Description,
   string HostId,
   List<UpdateMenuSectionCommand> Sections
) : IRequest<ErrorOr<Menu>>;

public record UpdateMenuSectionCommand(
   string? Id,
   string Name,
   string Description,
   List<UpdateMenuItemCommand> Items
);

public record UpdateMenuItemCommand(
   string? Id,
   string Name,
   string Description
);
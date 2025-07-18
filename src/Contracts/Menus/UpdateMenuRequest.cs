namespace Contracts.Menus;

public record UpdateMenuRequest(
   string Id,
   string Name,
   string Description,
   List<UpdateMenuSection> Sections
);

public record UpdateMenuSection(
   string? Id,
   string Name,
   string Description,
   List<UpdateMenuItem> Items
);

public record UpdateMenuItem(
   string? Id,
   string Name,
   string Description
);
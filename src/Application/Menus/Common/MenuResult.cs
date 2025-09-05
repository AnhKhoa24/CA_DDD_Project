namespace Application.Menus.Common;

public record MenuResult(
   int pageNumber,
   int pageSize,
   List<MenuCommandResult> Menus
);

public record MenuCommandResult(
   string Id,
   string Name,
   string Description,
   List<MenuSectionCommandResult> Sections
);

public record MenuSectionCommandResult(
   string? Id,
   string Name,
   string Description,
   List<MenuItemCommandResult> Items
);

public record MenuItemCommandResult(
   string? Id,
   string Name,
   string Description
);

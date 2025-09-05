namespace Contracts.Menus;

public record MenuListResponse(
   int pageNumber,
   int pageSize,
   List<MenuResponse> Menus
);
public record MenuResponse(
   string Id,
   string Name,
   string Description,
   float? AverageRating,
   List<MenuSectionResponse> Sections
);

public record MenuSectionResponse(
   string Id, 
   string Name,
   string Description,
   List<MenuItemResponse> Items
);

public record MenuItemResponse(
   string Id, 
   string Name,
   string Description);


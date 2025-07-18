using Domain.Menu;

namespace Application.Menus.Common;

public record MenuResult(
   int pageNumber,
   int pageSize,
   List<Menu> Menus
);
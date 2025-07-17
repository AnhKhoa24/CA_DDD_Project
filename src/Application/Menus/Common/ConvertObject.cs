using Domain.Menu.ValueObjects;

namespace Application.Menus.Common;

public static class ConvertObject
{
   public static MenuId StringToMenuId(this string? input)
   {
      return Guid.TryParse(input, out var guid) ? MenuId.Create(guid) : MenuId.Create(Guid.Empty); ;
   }
   public static MenuSectionId StringToMenuSectionId(this string? input)
   {
      return Guid.TryParse(input, out var guid) ? MenuSectionId.Create(guid) : MenuSectionId.Create(Guid.Empty);
   }
   public static MenuItemId StringToMenuItemId(this string? input)
   {
      return Guid.TryParse(input, out var guid) ? MenuItemId.Create(guid) : MenuItemId.Create(Guid.Empty);
   }
}
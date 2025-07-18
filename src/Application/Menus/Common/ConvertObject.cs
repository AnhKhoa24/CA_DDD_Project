using Domain.Menu;
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




   public static List<MenuCommandResult> ConvertMenuCommandResult(this List<Menu> menus)
   {
      return menus.ConvertAll(menu => new MenuCommandResult(
         Id: menu.Id.Value.ToString(),
         Name: menu.Name,
         Description: menu.Description,

         Sections: menu.Sections.Select(section => new MenuSectionCommandResult(
            Id: section.Id.Value.ToString(),
            Name: section.Name,
            Description: section.Description,
            
            Items: section.Items.Select(item => new MenuItemCommandResult(
               Id: item.Id.Value.ToString(),
               Name: item.Name,
               Description: item.Description
            )).ToList()
         )).ToList()
      ));
   }
}
using Application.Menus;
using UnitTests.Application.UnitTests.TestUtils.Constants;

namespace UnitTests.Application.UnitTests.Menus.Commands.TestUtils;

public static class CreateMenuCommandUtils
{
   public static CreateMenuCommand CreateCommand(
      List<MenuSectionCommand>? sections = null,
      string name = Constants.Menu.Name,
      string description = Constants.Menu.Description
   ) =>
      new CreateMenuCommand
      (
         name,
         description,
         Constants.Menu.HostId,
         sections ?? CreateSectionCommand()
      );

   public static List<MenuSectionCommand> CreateSectionCommand(
      int sectionCount =1,
      List<MenuItemCommand>? items = null
   ) =>
      Enumerable.Range(0, sectionCount)
         .Select(index => new MenuSectionCommand(
            Constants.Menu.SectionNameFromIndex(index),
            Constants.Menu.SectionDescriptionFromIndex(index),
            items ?? CreateItemsCommand()
         ))
         .ToList();
         
    public static List<MenuItemCommand> CreateItemsCommand(int itemCount = 1) =>
     Enumerable.Range(0, itemCount)
        .Select(index => new MenuItemCommand(
           Constants.Menu.ItemNameFromIndex(index),
           Constants.Menu.ItemDescriptionFromIndex(index)
        ))
        .ToList();
}
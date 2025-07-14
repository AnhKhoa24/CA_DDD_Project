using Application.Menus;
using Application.Menus.Commands.CreateMenu;
using FluentAssertions;
using UnitTests.Application.UnitTests.Menus.Commands.TestUtils;

namespace UnitTests.Application.UnitTests.Menus.Commands.CreateMenu;

public class MenuValidatorTest
{
   [Theory]
   [MemberData(nameof(InValidCreateMenuCommand))]
   public void HandleCreateMenu_WhenMenuIsIValid_ShouldReturnError(CreateMenuCommand createMenuCommand)
   {
      var validator = new CreateMenuCommandValidator();

      var result = validator.Validate(createMenuCommand);

      result.IsValid.Should().BeFalse();
      // result.Errors.Should().Contain(e => e.PropertyName == "Name");f
   }

   public static IEnumerable<object[]> InValidCreateMenuCommand()
   {
      yield return new[] { new CreateMenuCommandBuilder()
         .WithEmptyName()
         .Build() };

      yield return new[]
      {
         new CreateMenuCommandBuilder()
            .WithSections(
               new MenuSectionCommandBuilder().WithEmptyName().Build()
            )
         .Build()
      };

      yield return new[]
      {
         new CreateMenuCommandBuilder()
            .WithSections(
               new MenuSectionCommandBuilder()
                  .WithItems(new MenuItemCommandBuilder().WithEmptyDescription().Build())
                  .Build()
            )
            .Build()
      };
   }
}

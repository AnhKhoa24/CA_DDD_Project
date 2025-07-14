using Application.Common.Interfaces.Persistence;
using Application.Menus;
using FluentAssertions;
using Moq;
using UnitTests.Application.UnitTests.Menus.Commands.TestUtils;
using UnitTests.Application.UnitTests.TestUtils.Menus.Extensions;

namespace UnitTests.Application.UnitTests.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandlerTest
{
   //T1: SUT - logical component we're testing
   //T2: Scenario - what we're testing
   //T3: Expected outcome - what we expect the logical component to do

   private readonly CreateMenuCommandHandler _handler;
   private readonly Mock<IMenuRepository> _mockMenuRepository;

   public CreateMenuCommandHandlerTest()
   {
      _mockMenuRepository = new Mock<IMenuRepository>();
      _handler = new CreateMenuCommandHandler(_mockMenuRepository.Object);
   }

   [Theory]
   [MemberData(nameof(ValidCreateMenuCommand))]
   public async Task HandleCreateMenu_WhenMenuIsValid_ShouldCreateAndReturnMenu(CreateMenuCommand createMenuCommand)
   {
      var result = await _handler.Handle(createMenuCommand, default);

      result.IsError.Should().BeFalse();
      result.Value.ValidateCreatedFrom(createMenuCommand);

      _mockMenuRepository.Verify(m => m.AddMenuAsync(result.Value), Times.Once());
   }

   public static IEnumerable<object[]> ValidCreateMenuCommand()
   {
      yield return new[] { CreateMenuCommandUtils.CreateCommand() };
      yield return new[]
      {
         CreateMenuCommandUtils.CreateCommand(
            sections: CreateMenuCommandUtils.CreateSectionCommand(sectionCount:2)
         )
      };
      yield return new[]
      {
         CreateMenuCommandUtils.CreateCommand(
            sections: CreateMenuCommandUtils.CreateSectionCommand(
               sectionCount: 3,
               items : CreateMenuCommandUtils.CreateItemsCommand(itemCount: 2)
            )
         )
      };
   }

   // public void Test_HappyFlow() { }
   // public void Creating_A_Menu_Creates_And_Return_Menu() { }
   // public void Test_CreateMenuCommandHandler_HandleValid_ReturnsMenu() { }

}
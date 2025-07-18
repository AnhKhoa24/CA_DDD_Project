using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
   public static class Menu
   {
      public static Error MenuNotExists => Error.NotFound(code: "Menu.NotFound", description: "This menu is not exists.");
    }
}
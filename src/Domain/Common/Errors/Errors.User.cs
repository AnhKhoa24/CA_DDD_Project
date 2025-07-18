using ErrorOr;

namespace Domain.Common.Errors;

public static partial class Errors
{
   public static class User
   {
      public static Error DuplicateEmail => Error.Conflict(code: "User.Duplicate", description: "This email already exits.");
      public static Error InvalidCredentials => Error.Unauthorized(code: "User.InvalidCredentials", description: "Invalid Credentials.");
    }
}
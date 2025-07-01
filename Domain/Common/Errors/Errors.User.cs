using ErrorOr;

namespace Domain.Common.Errors;

public static class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(code: "User.Duplicate", description: "This email already exits.");
    }
}
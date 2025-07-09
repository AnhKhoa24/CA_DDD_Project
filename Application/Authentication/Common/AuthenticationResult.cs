
namespace Application.Authentication.Commmon;

public record AuthenticationResult(
   Guid Id,
   string FirstName,
   string LastName,
   string Email,
   string Token
);
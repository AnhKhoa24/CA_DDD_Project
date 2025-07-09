using Domain.User;

namespace Application.Authentication.Commmon;

public static class ConvertAuthentication
{
   public static AuthenticationResult Convert(User user, string token)
   {
      return new AuthenticationResult(
         user.Id.Value,
         user.FirstName,
         user.LastName,
         user.Email,
         token
      );
   }
}
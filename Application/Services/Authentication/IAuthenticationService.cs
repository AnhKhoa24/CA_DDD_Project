namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string FirstName, string LastName, string Email, string Password);
    AuthenticationResult Login(string Email, string Password);
}
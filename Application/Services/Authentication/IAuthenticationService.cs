namespace Application.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string FistName, string LastName,string Email, string Password);
}
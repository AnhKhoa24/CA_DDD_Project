using Application.Common.Interfaces.Authentication;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public AuthenticationResult Register(string FistName, string LastName, string Email, string Password)
    {
        //To do about 
        string token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), FistName, LastName);   
        return new AuthenticationResult(
            Guid.NewGuid(),
            FistName,
            LastName,
            Email,
            token
        );
    }
}

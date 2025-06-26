using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string FirstName, string LastName, string Email, string Password)
    {
        if (_userRepository.GetUserByEmail(Email) is not null)
        {
            throw new Exception("User with given email already exits.");
        }

        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password
        };
        _userRepository.AddUser(user);

        string token = _jwtTokenGenerator.GenerateToken(user.Id, FirstName, LastName);

        return new AuthenticationResult(
            user.Id,
            FirstName,
            LastName,
            Email,
            token
        );
    }
    public AuthenticationResult Login(string Email, string Password)
    {
        if (_userRepository.GetUserByEmail(Email) is not User user)
        {
            throw new Exception("User with email given does not exist.");
        }
        if (user.Password != Password)
        {
            throw new Exception("Invalid password.");
        }
        string token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);

        return new AuthenticationResult(
            user.Id,
            user.FirstName,
            user.LastName,
            Email,
            token
        );


    }

}

using Application.Common.Errors;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Services.Authentication.Commmon;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Services.Authentication.Commands.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) is not null)
        {
            return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };
        _userRepository.AddUser(user);

        string token = _jwtTokenGenerator.GenerateToken(user.Id, request.FirstName, request.LastName);

        return new AuthenticationResult(
            user,
            token
        );
    }
}

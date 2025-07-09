using Application.Authentication.Commmon;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Utils;
using Domain.Common.Errors;
using Domain.User;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.Register;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {        
        if (await _userRepository.GetUserByEmailAsync(request.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var passwordEncrypt = BcryptPasswordHasher.Hash(request.Password);

        var user = User.Create(request.FirstName, request.LastName, request.Email, passwordEncrypt);
        
        await _userRepository.AddUser(user);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user.Id.Value,
            user.FirstName,
            user.LastName,
            user.Email,
            token
        );
    }
}

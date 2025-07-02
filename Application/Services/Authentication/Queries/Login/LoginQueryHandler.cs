using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Services.Authentication.Commmon;
using Domain.Common.Errors;
using Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Services.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
   private readonly IUserRepository _userRepository;
   private readonly IJwtTokenGenerator _jwtTokenGenerator;

   public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
   {
      _userRepository = userRepository;
      _jwtTokenGenerator = jwtTokenGenerator;
   }

   public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
   {
     
      var verificationResult = await VerifyCredentialsAsync(request.Email, request.Password);

      if (verificationResult.IsError) return verificationResult.Errors;

      var token = _jwtTokenGenerator.GenerateToken(verificationResult.Value);

      return new AuthenticationResult(verificationResult.Value, token);
   }

   private async Task<ErrorOr<User>> VerifyCredentialsAsync(string email, string password)
   {
      if (await _userRepository.GetUserByEmailAsync(email) is not User user)
      {
         return Errors.User.InvalidCredentials;
      }

      if (user.Password != password)
      {
         return Errors.User.InvalidCredentials;
      }
      return user;
   }
}

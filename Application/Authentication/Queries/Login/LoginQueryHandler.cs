using Application.Authentication.Commmon;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Utils;
using Domain.Common.Errors;
using Domain.User;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login;

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
      var verificationResult = await VerifyCredentialsAsync(request);

      if (verificationResult.IsError) return verificationResult.Errors;

      var token = _jwtTokenGenerator.GenerateToken(verificationResult.Value);

      return ConvertAuthentication.Convert(verificationResult.Value, token);
   }

   private async Task<ErrorOr<User>> VerifyCredentialsAsync(LoginQuery request)
   {
      if (await _userRepository.GetUserByEmailAsync(request.Email) is not User user)
      {
         return Errors.User.InvalidCredentials;
      }

      var isPasswordCorrect = BcryptPasswordHasher.Verify(request.Password, user.Password);

      if (!isPasswordCorrect)
      {
         return Errors.User.InvalidCredentials;
      }

      return user;
   }
}

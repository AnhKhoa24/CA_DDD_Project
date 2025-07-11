using Application.Authentication.Commands.Register;
using Application.Authentication.Commmon;
using Application.Authentication.Queries.Login;
using Contractas.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controller;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
   private readonly ISender _sender;
   private readonly IMapper _mapper;

   public AuthenticationController(ISender sender, IMapper mapper)
   {
      _sender = sender;
      _mapper = mapper;
   }

   [HttpPost("register")]
   public async Task<IActionResult> Register(RegisterRequest registerRequest)
   {
      var registerCommand = _mapper.Map<RegisterCommand>(registerRequest);

      ErrorOr<AuthenticationResult> registerResult = await _sender.Send(registerCommand);

      return registerResult.Match(
          registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
          errors => Problem(errors)
      );
   }

   [HttpPost("login")]
   public async Task<IActionResult> Login(LoginRequest loginRequest)
   {
      var loginQuery = _mapper.Map<LoginQuery>(loginRequest);

      ErrorOr<AuthenticationResult> loginResult = await _sender.Send(loginQuery);

      return loginResult.Match(
         loginResult => Ok(_mapper.Map<AuthenticationResponse>(loginResult)),
         errors => Problem(errors)
      );
   }

   [HttpPost("error-test")]
   public IActionResult ErrorTest() => throw new Exception("This is the exception test.");

}
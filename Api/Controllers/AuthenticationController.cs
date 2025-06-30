using Application.Common.Errors;
using Application.Services.Authentication.Commands.Register;
using Application.Services.Authentication.Commmon;
using Contractas.Authentication;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controller;

[ApiController]
[Route("auth")]
// [ErrorHandingFilter]
public class AuthenticationController : ControllerBase
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

        Result<AuthenticationResult> registerResult = await _sender.Send(registerCommand);

        if (registerResult.IsSuccess)
        {
            return Ok(_mapper.Map<AuthenticationResponse>(registerResult.Value));
        }
        var firstError = registerResult.Errors[0];

        return firstError switch
        {
            DuplicateEmailError duplicate => Problem(
                statusCode: 409,
                title: duplicate.Message,
                detail: duplicate.Detail),

            _ => Problem()
        };
    }
    [HttpPost("error-test")]
    public IActionResult ErrorTest() => throw new Exception("This is the error test.");

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authenticationResult)
    {
        var resultAuthResponse = new AuthenticationResponse(
            authenticationResult.user.FirstName,
            authenticationResult.user.LastName,
            authenticationResult.user.Email,
            authenticationResult.token
        );
        return resultAuthResponse;
    }

}
using Application.Common.Errors;
using Application.Services.Authentication.Commands.Register;
using Application.Services.Authentication.Commmon;
using Contractas.Authentication;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controller;

[ApiController]
[Route("auth")]
// [ErrorHandingFilter]
public class AuthenticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest registerRequest)
    {
        var registerCommand = new RegisterCommand(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password);

        Result<AuthenticationResult> registerResult = await _sender.Send(registerCommand);

        if (registerResult.IsSuccess)
        {
            return Ok(MapAuthResut(registerResult.Value));
        }
        var firstError = registerResult.Errors[0];
        if (firstError is DuplicateEmailError duplicateEmailError)
        {
            return Problem(
                statusCode: 409,
                title: duplicateEmailError.Message,
                detail: duplicateEmailError.Detail);
        }

        return Problem();
    }
    [HttpPost("error-test")]
    public IActionResult ErrorTest() => throw new Exception("This is the error test.");

    private static AuthenticationResponse MapAuthResut(AuthenticationResult authenticationResult)
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
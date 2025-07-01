using Application.Services.Authentication.Commands.Register;
using Application.Services.Authentication.Commmon;
using Contractas.Authentication;
using ErrorOr;
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

        ErrorOr<AuthenticationResult> registerResult = await _sender.Send(registerCommand);


        return registerResult.MatchFirst(
            registerResult => Ok(_mapper.Map<AuthenticationResponse>(registerResult)),
            firstError => Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description)
        );

        /*==> Use with FluentResults*/
        // if (registerResult.IsSuccess) return Ok(_mapper.Map<AuthenticationResponse>(registerResult.Value));
        // var firstError = registerResult.Errors[0];
        // var errorList = registerResult.Errors.Select(s => s.Message);
        // return firstError switch
        // {
        //     IError error => Problem(statusCode: 409, title: error.Message, detail: JsonSerializer.Serialize(errorList)),
        //     _ => Problem()
        // };
    }
    [HttpPost("error-test")]
    public IActionResult ErrorTest() => throw new Exception("This is the error test.");

}
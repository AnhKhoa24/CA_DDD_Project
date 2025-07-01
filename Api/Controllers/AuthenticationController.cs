using Application.Services.Authentication.Commands.Register;
using Application.Services.Authentication.Commmon;
using Contractas.Authentication;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Api.Controller;

[Route("auth")]
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
    [HttpPost("error-test")]
    public IActionResult ErrorTest() => throw new Exception("This is the error test.");

}
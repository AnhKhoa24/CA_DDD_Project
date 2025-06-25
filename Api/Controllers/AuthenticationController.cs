
using Application.Services.Authentication;
using Contractas.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Api.Controller;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        var response = _authenticationService.Register(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email, registerRequest.Password);
        ///Mapping ...................

        return Ok(response);
    }
}
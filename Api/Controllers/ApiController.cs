using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        var firstError = errors[0];
        var statusCode = MapErrorTypeToStatusCode(firstError);
        
        var errorDescriptions = errors.Select(e => e.Description).ToList();

        var detailJson = System.Text.Json.JsonSerializer.Serialize(errorDescriptions);

        return Problem(
            statusCode: statusCode,
            title: firstError.Description,
            detail: detailJson 
        );
    }


    private int MapErrorTypeToStatusCode(Error error)
    {
        return error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
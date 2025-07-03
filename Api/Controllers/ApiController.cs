using Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api;

[ApiController]
public class ApiController : ControllerBase
{
   protected IActionResult Problem(List<Error> errors)
   {
      if (errors.Count is 0) return Problem();

      if (errors.All(error => error.Type is ErrorType.Validation))
      {
         return ValidationProblem(errors);
      }

      HttpContext.Items[HttpContextItemKeys.Errors] = errors;

      return Problem(errors[0]);
   }

   private IActionResult ValidationProblem(List<Error> errors)
   {
      var modelStateDictionary = new ModelStateDictionary();

      foreach (var error in errors)
      {
         modelStateDictionary.AddModelError(error.Code, error.Description);
      }
      return ValidationProblem(modelStateDictionary);
   }

   private IActionResult Problem(Error error)
   {
      return error.Type switch
      {
         ErrorType.Conflict => Problem(statusCode: StatusCodes.Status409Conflict, title: error.Description),
         ErrorType.NotFound => Problem(statusCode: StatusCodes.Status404NotFound, title: error.Description),
         ErrorType.Validation => Problem(statusCode: StatusCodes.Status400BadRequest, title: error.Description),
         ErrorType.Unauthorized => Problem(statusCode: StatusCodes.Status401Unauthorized, title: error.Description),
         ErrorType.Forbidden => Problem(statusCode: StatusCodes.Status403Forbidden, title: error.Description),
         _ => Problem(),
      };
   }
}
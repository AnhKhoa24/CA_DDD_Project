using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/[controller]")]
public class LunchController : ApiController
{
   [HttpGet("ListLunch")]
   public ActionResult ListLunch()
   {
      return Ok(Array.Empty<string>());
   }
}

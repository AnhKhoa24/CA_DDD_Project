using System.Threading.Tasks;
using Application.Menus;
using Contracts.Menus;
using Domain.Menu;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/{hostId}/menu")]
public class MenuController : ApiController
{
   private readonly IMapper _mapper;
   private readonly ISender _sender;

   public MenuController(IMapper mapper, ISender sender)
   {
      _mapper = mapper;
      _sender = sender;
   }
   [HttpPost]
   public async Task<IActionResult> CreateMenu(
      CreateMenuRequest request,
      string hostId
   )
   {
      var command = _mapper.Map<CreateMenuCommand>((request, hostId));
      ErrorOr<Menu> createMenuResult = await _sender.Send(command);

      return createMenuResult.Match(
          createMenuResult => Ok(createMenuResult),
          errors => Problem(errors)
      );   
   }
}
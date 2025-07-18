using Application.Menus;
using Application.Menus.Commands.UpdateMenu;
using Application.Menus.Queries.GetMenuPaginate;
using Contracts.Menus;
using Domain.Menu;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/{hostId}/menu")]
[AllowAnonymous]
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
          createMenuResult => Ok(_mapper.Map<MenuResponse>(createMenuResult)),
          errors => Problem(errors)
      );
   }
   [HttpGet]
   public async Task<IActionResult> GetMenuPaginate(string hostId, int pageNumber = 1, int pageSize = 10)
   {
      var command = new GetMenuPaginateQuery(pageNumber, pageSize);
      var queryResult = await _sender.Send(command);
      return Ok(_mapper.Map<MenuListResponse>(queryResult));
   }
   [HttpPut]
   public async Task<IActionResult> UpdateMenu(
      UpdateMenuRequest request,
      string hostId
   )
   {
      var command = _mapper.Map<UpdateMenuCommand>((request, hostId));
      ErrorOr<Menu> updateMenuResult = await _sender.Send(command);

      return updateMenuResult.Match(
          updateMenuResult => Ok(_mapper.Map<MenuResponse>(updateMenuResult)),
          errors => Problem(errors)
      );
   }
}
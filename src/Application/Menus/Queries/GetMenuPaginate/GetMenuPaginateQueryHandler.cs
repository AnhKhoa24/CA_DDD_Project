using Application.Common.Interfaces.Persistence;
using Application.Menus.Common;
using ErrorOr;
using Application.Common.Messaging;

namespace Application.Menus.Queries.GetMenuPaginate;

public class GetMenuPaginateQueryHandler : IQueryHandler<GetMenuPaginateQuery, MenuResult>
{
   private readonly IMenuRepository _menuRepository;

   public GetMenuPaginateQueryHandler(IMenuRepository menuRepository)
   {
      _menuRepository = menuRepository;
   }

   public async Task<ErrorOr<MenuResult>> Handle(GetMenuPaginateQuery request, CancellationToken cancellationToken)
   {
      var menus = await _menuRepository.GetMenusPaginateAsync(request.pageNumber, request.pageSize);
      return new MenuResult(request.pageSize, request.pageNumber, menus.ConvertMenuCommandResult());
   }
}


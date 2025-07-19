using Application.Common.Interfaces.Persistence;
using Application.Menus.Common;
using MediatR;
using Application.Menus.Cache;
using Application.Common.Cache.Interfaces;

namespace Application.Menus.Queries.GetMenuPaginate;

public class GetMenuPaginateQueryHandler : IRequestHandler<GetMenuPaginateQuery, MenuResult>
{
   private readonly IMenuRepository _menuRepository;
   private readonly IGenericCacheService _genericCacheService;

   public GetMenuPaginateQueryHandler(IMenuRepository menuRepository, IGenericCacheService genericCacheService)
   {
      _menuRepository = menuRepository;
      _genericCacheService = genericCacheService;
   }

   public async Task<MenuResult> Handle(GetMenuPaginateQuery request, CancellationToken cancellationToken)
   {
      var cacheKey = MenuCacheSettings.GetCacheMenuPaginateKey(request.pageNumber, request.pageSize);
      var groupKey = MenuCacheSettings.GetGroupKey();

      return await _genericCacheService.GetOrAddAsync(
          cacheKey,
          groupKey,
          async () =>
          {
             var menus = await _menuRepository.GetMenusPaginateAsync(request.pageNumber, request.pageSize);
             return new MenuResult(request.pageSize, request.pageNumber, menus.ConvertMenuCommandResult());
          },
          TimeSpan.FromMinutes(5),
          cancellationToken);
   }
}


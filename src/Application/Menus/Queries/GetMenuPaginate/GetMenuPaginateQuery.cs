using Application.Common.Messaging;
using Application.Menus.Cache;
using Application.Menus.Common;

namespace Application.Menus.Queries.GetMenuPaginate;

public record GetMenuPaginateQuery(int pageNumber = 1, int pageSize = 10) : ICachedQuery<MenuResult>
{
   public string Key => MenuCacheSettings.GetCacheMenuPaginateKey(pageSize, pageNumber);

   public TimeSpan? Expiration => MenuCacheSettings.DefaultExpiration;

   public string KeyGroup => MenuCacheSettings.KeyGroup;
}

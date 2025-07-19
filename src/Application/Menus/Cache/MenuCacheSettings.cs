namespace Application.Menus.Cache;

public static class MenuCacheSettings
{
   private const string CacheMenuPaginateKey = "menu_page";
   public static string GetCacheMenuPaginateKey(int pageNumber, int pageSize)
      => $"{CacheMenuPaginateKey}:{pageNumber}&size={pageSize}";
   public static string GetGroupKey() => $"manage_{CacheMenuPaginateKey}";
}
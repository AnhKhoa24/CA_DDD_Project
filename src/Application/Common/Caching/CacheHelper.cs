using Application.Common.Caching;
using ErrorOr;

internal sealed class CacheHelper : ICacheHelper
{
   private readonly ICacheProvider _cache;

   public CacheHelper(ICacheProvider cache)
   {
      _cache = cache;
   }

   public async Task<ErrorOr<T>> GetOrSetAsync<T>(
       string key,
       string keyGroup,
       TimeSpan? expiration,
       Func<Task<ErrorOr<T>>> factory,
       CancellationToken cancellationToken = default)
   {
      var cached = await _cache.GetAsync<T>(key, cancellationToken);
      if (cached is not null)
      {
         return cached;
      }

      var result = await factory();

      if (!result.IsError)
      {
         await _cache.SetAsync(key, result.Value, expiration, cancellationToken);
         await AddKeyToGroupKeyAsync(keyGroup, key, expiration, cancellationToken);
      }

      return result;
   }
   private async Task AddKeyToGroupKeyAsync(string groupKey, string key, TimeSpan? ttl, CancellationToken cancellationToken)
   {
      var keyList = await _cache.GetAsync<HashSet<string>>(groupKey, cancellationToken) ?? new HashSet<string>();
      keyList.Add(key);
      await _cache.SetAsync(groupKey, keyList, ttl, cancellationToken);
   }
   public async Task ClearCacheFromGroupKeyAsync(string groupKey, CancellationToken cancellationToken)
   {
      var keyList = await _cache.GetAsync<HashSet<string>>(groupKey, cancellationToken);
      if (keyList is null) return;

      foreach (var key in keyList)
      {
         await _cache.RemoveAsync(key, cancellationToken);
      }
      await _cache.RemoveAsync(groupKey, cancellationToken);
   }
}

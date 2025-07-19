using Application.Common.Cache.Interfaces;

namespace Application.Common.Cache.Core;

public class GenericCacheService : IGenericCacheService
{
   private readonly ICacheProvider _cacheProvider;

   public GenericCacheService(ICacheProvider cacheProvider)
   {
      _cacheProvider = cacheProvider;
   }

   ///pattern Cache-Aside
   public async Task<T> GetOrAddAsync<T>(string key, string groupKey, Func<Task<T>> factory,
   TimeSpan ttl, CancellationToken cancellationToken)
   {
      var cached = await _cacheProvider.GetAsync<T>(key, cancellationToken);
      if (cached is not null)
         return cached;

      var data = await factory();

      await _cacheProvider.SetAsync(key, data, ttl, cancellationToken);
      await AddKeyToGroupKeyAsync(groupKey, key, ttl, cancellationToken);

      return data;
   }

   private async Task AddKeyToGroupKeyAsync(string groupKey, string key, TimeSpan ttl, CancellationToken cancellationToken)
   {
      var keyList = await _cacheProvider.GetAsync<HashSet<string>>(groupKey, cancellationToken) ?? new HashSet<string>();
      keyList.Add(key);
      await _cacheProvider.SetAsync(groupKey, keyList, ttl, cancellationToken);
   }
   public async Task ClearCacheFromGroupKeyAsync(string groupKey, CancellationToken cancellationToken)
   {
      var keyList = await _cacheProvider.GetAsync<HashSet<string>>(groupKey, cancellationToken);
      if (keyList is null) return;

      foreach (var key in keyList)
      {
         await _cacheProvider.RemoveAsync(key, cancellationToken);
      }
      //  await _cacheProvider.RemoveAsync(key, cancellationToken);
   }
}


using System.Text;
using Application.Common.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Infrastructure.Cache;

internal class RedisCacheProvider : ICacheProvider
{
   private readonly IDistributedCache _distributedCache;

   public RedisCacheProvider(IDistributedCache distributedCache)
   {
      _distributedCache = distributedCache;
   }

   public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
   {
      var cachedBytes = await _distributedCache.GetAsync(key, cancellationToken);
      if (cachedBytes == null)
         return default;

      var json = Encoding.UTF8.GetString(cachedBytes);
      return JsonConvert.DeserializeObject<T>(json);
   }

   public async Task SetAsync<T>(string key, T value, TimeSpan? expiration, CancellationToken cancellationToken = default)
   {
      var json = JsonConvert.SerializeObject(value);
      var bytes = Encoding.UTF8.GetBytes(json);

      var options = new DistributedCacheEntryOptions
      {
         AbsoluteExpirationRelativeToNow = expiration
      };

      await _distributedCache.SetAsync(key, bytes, options, cancellationToken);
   }

   public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
   {
      await _distributedCache.RemoveAsync(key, cancellationToken);
   }
}
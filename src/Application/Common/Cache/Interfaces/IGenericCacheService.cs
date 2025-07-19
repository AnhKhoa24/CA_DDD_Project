namespace Application.Common.Cache.Interfaces;

public interface IGenericCacheService
{
   Task<T> GetOrAddAsync<T>(string key, string groupKey,
      Func<Task<T>> factory, TimeSpan ttl, CancellationToken cancellationToken);

   Task ClearCacheFromGroupKeyAsync(string groupKey, CancellationToken cancellationToken);
}

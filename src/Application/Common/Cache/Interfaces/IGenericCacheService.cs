namespace Application.Common.Cache.Interfaces;
public interface IGenericCacheService
{
   Task<T> GetOrAddAsync<T>(string key,
   Func<Task<T>> factory, TimeSpan ttl, CancellationToken cancellationToken);
}

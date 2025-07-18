using Application.Common.Cache.Interfaces;

namespace Application.Common.Cache.Core;

public class GenericCacheService : IGenericCacheService
{
    private readonly ICacheProvider _cacheProvider;

    public GenericCacheService(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> factory, TimeSpan ttl, CancellationToken cancellationToken)
    {
        var cached = await _cacheProvider.GetAsync<T>(key, cancellationToken);
        if (cached is not null)
            return cached;

        var data = await factory();
        await _cacheProvider.SetAsync(key, data, ttl, cancellationToken);
        return data;
    }
}

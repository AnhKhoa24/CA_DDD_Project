using ErrorOr;

namespace Application.Common.Caching;

internal interface ICacheHelper
{
   Task<ErrorOr<T>> GetOrSetAsync<T>(
       string key,
       string keyGroup,
       TimeSpan? expiration,
       Func<Task<ErrorOr<T>>> factory,
       CancellationToken cancellationToken = default);

   Task ClearCacheFromGroupKeyAsync(string groupKey, CancellationToken cancellationToken);
}

using Application.Common.Caching;
using Application.Common.Messaging;
using ErrorOr;
using MediatR;

namespace Application.Common.Behaviors;

// internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>
//    : IPipelineBehavior<TRequest, TResponse>
//    where TRequest : ICachedQuery
// {
//    private readonly ICacheService _cacheService;

//    public QueryCachingPipelineBehavior(ICacheService cacheService)
//    {
//       _cacheService = cacheService;
//    }

//    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
//       CancellationToken cancellationToken)
//    {
//       return await _cacheService.GetOrCreateAsync(
//          request.Key,
//          _ => next(),
//          request.Expiration,
//          cancellationToken
//       );
//    }
// }

internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
{
   private readonly ICacheHelper _cacheHelper;

   public QueryCachingPipelineBehavior(ICacheHelper cacheHelper)
   {
      _cacheHelper = cacheHelper;
   }

   public async Task<TResponse> Handle(
       TRequest request,
       RequestHandlerDelegate<TResponse> next,
       CancellationToken cancellationToken)
   {
      return await PipelineReflectionHelper.InvokeWithGenericInnerType(
         this,
         nameof(HandleInternal),
         request,
         next,
         cancellationToken
     );
   }

   private async Task<TResponse> HandleInternal<TActual>(
       TRequest request,
       RequestHandlerDelegate<TResponse> next,
       CancellationToken cancellationToken)
   {
      var result = await _cacheHelper.GetOrSetAsync(
         key: request.Key,
         keyGroup: request.KeyGroup,
         expiration: request.Expiration,
         async () => (ErrorOr<TActual>)(object)(await next())!,
         cancellationToken);

      return (TResponse)(object)result;
   }
}


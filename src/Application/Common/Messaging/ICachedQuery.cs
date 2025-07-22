

using ErrorOr;
using MediatR;

namespace Application.Common.Messaging;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery;
// public interface ICachedQuery<TResponse> : IRequest<ErrorOr<TResponse>>, ICachedQuery
// {
// }
public interface ICachedQuery
{
   string Key { get; }
   string KeyGroup  { get; }
   TimeSpan? Expiration { get; }
}
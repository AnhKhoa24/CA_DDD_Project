using ErrorOr;
using MediatR;

namespace Application.Common.Messaging;

public interface IQueryHandler<TQuery, TResponse>
   : IRequestHandler<TQuery, ErrorOr<TResponse>>
   where TQuery : IQuery<TResponse>
{ }

// public interface IQueryHandler<TQuery, TResponse> 
//     : IRequestHandler<TQuery, ErrorOr<TResponse>>
//     where TQuery : IRequest<ErrorOr<TResponse>>
// { }

using ErrorOr;
using MediatR;

namespace Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{ }


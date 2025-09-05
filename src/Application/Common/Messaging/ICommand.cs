using ErrorOr;
using MediatR;

namespace Application.Common.Messaging;

public record Success;

public interface ICommand : IRequest<ErrorOr<Success>>
{ }

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
   
}
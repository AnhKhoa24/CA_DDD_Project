using Application.Authentication.Commmon;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.Login;


public record LoginQuery
(
   string Email,
   string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
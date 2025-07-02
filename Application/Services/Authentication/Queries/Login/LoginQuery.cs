using Application.Services.Authentication.Commmon;
using ErrorOr;
using MediatR;

namespace Application.Services.Authentication.Queries.Login;


public record LoginQuery
(
   string Email,
   string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
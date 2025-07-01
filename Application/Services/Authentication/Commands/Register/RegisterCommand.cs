using Application.Services.Authentication.Commmon;
using ErrorOr;
using MediatR;

namespace Application.Services.Authentication.Commands.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;
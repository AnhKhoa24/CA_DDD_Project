using Domain.Entities;

namespace Application.Services.Authentication.Commmon;

public record AuthenticationResult(
    User user,
    string token
);
namespace Contractas.Authentication;

public record LoginRequest
(
    string Email,
    string Password
);
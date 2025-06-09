namespace FrontEnd.Api.Features.Account.Contracts.Responses;

public record LoginResponse(
    int Id,
    string Username,
    string Email,
    string Role,
    string Token
);

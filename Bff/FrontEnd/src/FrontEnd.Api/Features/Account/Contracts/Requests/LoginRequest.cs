namespace FrontEnd.Api.Features.Account.Contracts.Requests;

public record LoginRequest(
    string Username,
    string Password
);

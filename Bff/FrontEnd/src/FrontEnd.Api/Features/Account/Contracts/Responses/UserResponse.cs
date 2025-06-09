namespace FrontEnd.Api.Features.Account.Contracts.Responses;

public record UserResponse(
    int Id,
    string Username,
    string Email,
    string Role
); 
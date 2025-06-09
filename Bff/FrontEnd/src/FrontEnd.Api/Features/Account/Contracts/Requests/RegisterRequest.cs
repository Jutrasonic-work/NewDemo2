namespace FrontEnd.Api.Features.Account.Contracts.Requests;

public record RegisterRequest
    (
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
);
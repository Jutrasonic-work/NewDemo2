using FrontEnd.Api.Features.Account.Contracts.Requests;
using FrontEnd.Api.Features.Account.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Api.Features.Account.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var accountGroup = app.MapGroup("/api/account")
            .WithTags("Account");

        // Login to the application
        accountGroup.MapPost("/Login",
            async ([FromBody] LoginRequest request, [FromServices] LoginUseCase useCase) =>
            {
                var result = await useCase.ExecuteAsync(request);
                return Results.Ok(result);
            })
            .WithSummary("Login")
            .WithDescription("Authenticates a user and returns a JWT token. The user credentials are provided in the request body.");

        // Register a new user
        accountGroup.MapPost("/Register",
            async ([FromBody] RegisterRequest request, [FromServices] RegisterUseCase useCase) =>
            {
                await useCase.ExecuteAsync(request);
                return Results.Created();
            })
            .WithSummary("Register")
            .WithDescription("Registers a new user. The user details are provided in the request body.");

        //accountGroup.MapPost("/RefreshToken",
        //    async ([FromBody] RefreshTokenRequest request, [FromServices] RefreshTokenUseCase useCase) =>
        //    {
        //        var result = await useCase.Execute(request);
        //        return Results.Ok(result);
        //    });

        // Logout from the application
        accountGroup.MapPost("/Logout",
            async ([FromServices] LogoutUseCase useCase) =>
            {
                await useCase.ExecuteAsync();
                return Results.NoContent();
            })
            .WithSummary("Logout")
            .WithDescription("Logs out the current user and invalidates the JWT token.");
    }
}
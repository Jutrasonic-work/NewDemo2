namespace FrontEnd.Api.Features.Shop.Contracts.Responses;

public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    string CategoryName,
    string Unit,
    bool IsAvailable,
    string ImageUrl,
    decimal Rating); 
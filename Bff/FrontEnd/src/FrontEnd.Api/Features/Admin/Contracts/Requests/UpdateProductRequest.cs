namespace FrontEnd.Api.Features.Admin.Contracts.Requests;

public record UpdateProductRequest(
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    string Unit,
    bool IsAvailable,
    string ImageUrl,
    decimal Rating); 
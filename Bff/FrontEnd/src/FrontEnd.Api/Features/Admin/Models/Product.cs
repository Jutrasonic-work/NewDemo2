namespace FrontEnd.Api.Features.Admin.Models;

public record Product(
    int Id,
    string Name,
    string Description,
    decimal Price,
    int CategoryId,
    string Unit,
    bool IsAvailable,
    string ImageUrl,
    decimal Rating); 
using Catalog.Domain.Enums;

namespace Catalog.Application.Features.Products.Contracts.Requests;

public record UpdateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; } = 0.0m;
    public string ImageUrl { get; init; } = string.Empty;
    public Guid CategoryId { get; init;  } = Guid.Empty;
    public int Stock { get; init; } = 0;
    public ProductUnit Unit { get; init; } = ProductUnit.Piece;
    public bool InStock { get; init; } = true;
}
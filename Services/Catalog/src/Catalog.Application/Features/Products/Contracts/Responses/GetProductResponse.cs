using Catalog.Application.Features.Categories.Contracts.Responses;
using Catalog.Domain.Enums;

namespace Catalog.Application.Features.Products.Contracts.Responses;

public record GetProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
    public CategoryInfo Category { get; init; } = new CategoryInfo();
    public int Stock { get; init; } = 0;
    public ProductUnit Unit { get; init; } = ProductUnit.Piece;
    public bool InStock { get; init; } = false;
}

namespace Catalog.Application.Features.Products.Contracts.Responses;

public record GetProductsResponse
{
    public IEnumerable<GetProductsItemResponse> Items { get; init; } = Enumerable.Empty<GetProductsItemResponse>();
}

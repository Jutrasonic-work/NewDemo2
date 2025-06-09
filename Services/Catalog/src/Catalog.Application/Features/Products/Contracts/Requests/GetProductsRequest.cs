namespace Catalog.Application.Features.Products.Contracts.Requests;


public record GetProductsRequest
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}

namespace FrontEnd.Api.Features.Shop.Contracts.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public ProductCategoryResponse Category { get; set; } = new();
    public int Unit { get; set; }
    public bool InStock { get; set; }
}
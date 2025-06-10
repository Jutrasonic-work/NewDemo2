namespace FrontEnd.Api.Features.Shop.Contracts.Responses;

public class ProductListResponse
{
    public List<ProductResponse> Items { get; set; } = new();
}


namespace FrontEnd.Api.Features.Shop.Contracts.Requests;

public class AddToOrderRequest
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string ProductName { get; set; } = string.Empty;
} 
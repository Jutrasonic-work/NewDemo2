namespace FrontEnd.Api.Features.Shop.Contracts.Requests;

public class UpdateOrderRequest
{
    public int Id { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public List<OrderItemRequest> Items { get; set; } = new();
    public string Status { get; set; } = string.Empty;
} 
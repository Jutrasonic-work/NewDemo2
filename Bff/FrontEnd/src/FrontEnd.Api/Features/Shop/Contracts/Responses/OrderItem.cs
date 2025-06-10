namespace FrontEnd.Api.Features.Shop.Contracts.Responses;

public record OrderItem(
    int Id,
    int ProductId,
    string Name,
    decimal Price,
    int Quantity
); 
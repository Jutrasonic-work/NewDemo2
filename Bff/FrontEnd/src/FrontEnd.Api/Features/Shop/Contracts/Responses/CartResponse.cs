namespace FrontEnd.Api.Features.Shop.Contracts.Responses;

public record CartResponse(
    int Id,
    string UserId,
    decimal TotalPrice,
    IEnumerable<CartItem> Items
); 
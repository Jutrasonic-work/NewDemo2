namespace FrontEnd.Api.Features.Shop.Contracts.Requests;

public record UpdateCartItemRequest(
    int ProductId,
    int Quantity
); 
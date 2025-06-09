namespace FrontEnd.Api.Features.Shop.Contracts.Requests;

public record AddToCartRequest(
    int ProductId,
    int Quantity
); 
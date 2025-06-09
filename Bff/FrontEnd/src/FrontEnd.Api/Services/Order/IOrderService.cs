using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;

namespace FrontEnd.Api.Services.Order;

public interface IOrderService
{
    Task<CartResponse> GetCartAsync(int id);
    Task AddToCartAsync(AddToCartRequest request);
    Task UpdateCartItemAsync(int id, UpdateCartItemRequest request);
    Task RemoveFromCartAsync(int id);
    Task ClearCartAsync();
} 
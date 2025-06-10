using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;

namespace FrontEnd.Api.Services.Order;

public interface IOrderService
{
    // Création et gestion des commandes
    Task<Guid> CreateOrderAsync(CreateOrderRequest request);
    Task<OrderResponse> GetOrderAsync(int id);
    Task<IEnumerable<OrderResponse>> GetOrdersAsync(int userId);
    Task UpdateOrderAsync(int id, UpdateOrderRequest request);
    Task DeleteOrderAsync(int id);

    // Gestion des articles dans une commande
    Task AddToOrderAsync(int orderId, AddToOrderRequest request);
    Task RemoveFromOrderAsync(int orderId, int itemId);
} 
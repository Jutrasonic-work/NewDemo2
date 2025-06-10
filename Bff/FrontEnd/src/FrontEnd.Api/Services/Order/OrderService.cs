using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;
using System.Net.Http.Json;

namespace FrontEnd.Api.Services.Order;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("OrderApi");
        _logger = logger;
    }

    public async Task<OrderResponse> GetOrderAsync(int id)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.GetFromJsonAsync<OrderResponse>($"/api/orders/{guidId}");
            if (response is null)
            {
                throw new KeyNotFoundException($"La commande {id} n'existe pas");
            }
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération de la commande {OrderId}", id);
            throw;
        }
    }

    public async Task<Guid> CreateOrderAsync(CreateOrderRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/orders", new
            {
                UserId = request.UserId,
                Items = request.Items.Select(item => new
                {
                    ProductId = ConvertIntToGuid(item.ProductId),
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            });

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création de la commande pour l'utilisateur {UserId}", request.UserId);
            throw;
        }
    }

    public async Task UpdateOrderAsync(int id, UpdateOrderRequest request)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.PutAsJsonAsync($"/api/orders/{guidId}", new
            {
                Id = guidId,
                Items = request.Items.Select(item => new
                {
                    ProductId = ConvertIntToGuid(item.ProductId),
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            });

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour de la commande {OrderId}", id);
            throw;
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.DeleteAsync($"/api/orders/{guidId}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression de la commande {OrderId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<OrderResponse>> GetOrdersAsync(int userId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderResponse>>($"/api/orders?userId={userId}");
            return response ?? Enumerable.Empty<OrderResponse>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des commandes de l'utilisateur {UserId}", userId);
            throw;
        }
    }

    private static Guid ConvertIntToGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public Task AddToOrderAsync(int orderId, AddToOrderRequest request)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromOrderAsync(int orderId, int itemId)
    {
        throw new NotImplementedException();
    }
} 
using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;
using System.Net.Http.Json;

namespace FrontEnd.Api.Services.Order;

public class OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger) : IOrderService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("OrderApi");

    public async Task<CartResponse> GetCartAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CartResponse>($"/api/cart/{id}")
                ?? throw new InvalidOperationException("Le panier est vide");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération du panier");
            throw;
        }
    }

    public async Task AddToCartAsync(AddToCartRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/cart/items", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'ajout au panier");
            throw;
        }
    }

    public async Task UpdateCartItemAsync(int id, UpdateCartItemRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/cart/items/{id}", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la mise à jour de l'article {ItemId}", id);
            throw;
        }
    }

    public async Task RemoveFromCartAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/cart/items/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la suppression de l'article {ItemId}", id);
            throw;
        }
    }

    public async Task ClearCartAsync()
    {
        try
        {
            var response = await _httpClient.DeleteAsync("/api/cart");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la vidange du panier");
            throw;
        }
    }
} 
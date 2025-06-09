using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;
using System.Net.Http.Json;

namespace FrontEnd.Api.Services.Catalog;

public class CatalogService(IHttpClientFactory httpClientFactory, ILogger<CatalogService> logger) : ICatalogService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("CatalogApi");

    // Admin
    public async Task<int> CreateProductAsync(CreateProductRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/products", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la création du produit");
            throw;
        }
    }

    public async Task UpdateProductAsync(int id, UpdateProductRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/products/{id}", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la mise à jour du produit {ProductId}", id);
            throw;
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/products/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la suppression du produit {ProductId}", id);
            throw;
        }
    }

    public async Task<int> CreateCategoryAsync(CreateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/categories", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<int>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la création de la catégorie");
            throw;
        }
    }

    public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest request)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/categories/{id}", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la mise à jour de la catégorie {CategoryId}", id);
            throw;
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"/api/categories/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la suppression de la catégorie {CategoryId}", id);
            throw;
        }
    }

    // Shop
    public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductResponse>>("/api/products")
                ?? Enumerable.Empty<ProductResponse>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des produits");
            throw;
        }
    }

    public async Task<ProductResponse?> GetProductAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<ProductResponse>($"/api/products/{id}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération du produit {ProductId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryResponse>>("/api/categories")
                ?? Enumerable.Empty<CategoryResponse>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des catégories");
            throw;
        }
    }

    public async Task<CategoryResponse?> GetCategoryAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CategoryResponse>($"/api/categories/{id}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération de la catégorie {CategoryId}", id);
            throw;
        }
    }

    public async Task<IEnumerable<ProductResponse>> GetCategoryProductsAsync(int categoryId)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ProductResponse>>($"/api/categories/{categoryId}/products")
                ?? Enumerable.Empty<ProductResponse>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des produits de la catégorie {CategoryId}", categoryId);
            throw;
        }
    }
} 
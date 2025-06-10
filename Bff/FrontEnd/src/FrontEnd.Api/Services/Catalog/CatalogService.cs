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
            var guidId = await response.Content.ReadFromJsonAsync<Guid>();
            // Conversion sécurisée de Guid vers int en utilisant les 4 derniers octets
            return BitConverter.ToInt32(guidId.ToByteArray().TakeLast(4).ToArray(), 0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la création du produit {@Request}", request);
            throw;
        }
    }

    public async Task UpdateProductAsync(int id, UpdateProductRequest request)
    {
        try
        {
            // Conversion de l'ID en Guid pour l'API
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.PutAsJsonAsync($"/api/products/{guidId}", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la mise à jour du produit {ProductId} avec {@Request}", id, request);
            throw;
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.DeleteAsync($"/api/products/{guidId}");
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
            var guidId = await response.Content.ReadFromJsonAsync<Guid>();
            return BitConverter.ToInt32(guidId.ToByteArray().TakeLast(4).ToArray(), 0);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la création de la catégorie {@Request}", request);
            throw;
        }
    }

    public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest request)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.PutAsJsonAsync($"/api/categories/{guidId}", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la mise à jour de la catégorie {CategoryId} avec {@Request}", id, request);
            throw;
        }
    }

    public async Task DeleteCategoryAsync(int id)
    {
        try
        {
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.DeleteAsync($"/api/categories/{guidId}");
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
            var response = await _httpClient.GetFromJsonAsync<ProductListResponse>("/api/products");
            return response?.Items ?? Enumerable.Empty<ProductResponse>();
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
            var guidId = ConvertIntToGuid(id);
            var response = await _httpClient.GetFromJsonAsync<ProductResponse>($"/api/products/{guidId}");
            return response;
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
            var response = await _httpClient.GetFromJsonAsync<CategoryListResponse>("/api/categories");
            return response?.Items ?? Enumerable.Empty<CategoryResponse>();
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
            var guidId = ConvertIntToGuid(id);
            return await _httpClient.GetFromJsonAsync<CategoryResponse>($"/api/categories/{guidId}");
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
            var guidId = ConvertIntToGuid(categoryId);
            var response = await _httpClient.GetFromJsonAsync<ProductListResponse>($"/api/products?categoryId={guidId}");
            return response?.Items ?? Enumerable.Empty<ProductResponse>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la récupération des produits de la catégorie {CategoryId}", categoryId);
            throw;
        }
    }

    // Méthode utilitaire pour convertir un int en Guid
    private static Guid ConvertIntToGuid(int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
} 
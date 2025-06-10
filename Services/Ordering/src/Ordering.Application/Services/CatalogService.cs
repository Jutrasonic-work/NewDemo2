using Microsoft.Extensions.Configuration;
using Ordering.Application.Contracts;
using Ordering.Domain.Dtos;
using System.Net.Http;
using System.Net.Http.Json;

namespace Ordering.Application.Services;

public class CatalogService(HttpClient httpClient, IConfiguration configuration) : ICatalogService
{
    //public CatalogService()
    //{
    //    httpClient.BaseAddress = new Uri(configuration["CatalogApi:BaseUrl"] ?? "http://localhost:5000");
    //}
    public async Task<ProductDto?> GetProductById(Guid productId)
    {
        try
        {
            var response = await httpClient.GetAsync($"/api/products/{productId}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }
        catch
        {
            return null;
        }
    }
} 
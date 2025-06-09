using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Features.Shop.Contracts.Responses;

namespace FrontEnd.Api.Services.Catalog;

public interface ICatalogService
{
    // Admin
    Task<int> CreateProductAsync(CreateProductRequest request);
    Task UpdateProductAsync(int id, UpdateProductRequest request);
    Task DeleteProductAsync(int id);
    Task<int> CreateCategoryAsync(CreateCategoryRequest request);
    Task UpdateCategoryAsync(int id, UpdateCategoryRequest request);
    Task DeleteCategoryAsync(int id);

    // Shop
    Task<IEnumerable<ProductResponse>> GetProductsAsync();
    Task<ProductResponse?> GetProductAsync(int id);
    Task<IEnumerable<CategoryResponse>> GetCategoriesAsync();
    Task<CategoryResponse?> GetCategoryAsync(int id);
    Task<IEnumerable<ProductResponse>> GetCategoryProductsAsync(int categoryId);
} 
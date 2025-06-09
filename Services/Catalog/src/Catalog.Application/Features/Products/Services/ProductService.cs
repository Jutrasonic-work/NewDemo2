using Catalog.Application.Features.Products.Contracts.Requests;
using Catalog.Application.Features.Products.Contracts.Responses;
using Catalog.Application.Features.Products.Interfaces;
using Catalog.Application.Mappers;

namespace Catalog.Application.Features.Products.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    // Gets a list of products
    public async Task<GetProductsResponse> GetListAsync(GetProductsRequest request)
    {
        var products = await productRepository.GetListAsync();
        return new GetProductsResponse { Items = ProductMappers.MapToResponse(products) };
    }

    // Gets a product by ID
    public async Task<GetProductResponse> GetAsync(Guid id)
    {
        var product = await productRepository.GetAsync(id);
        return ProductMappers.MapToResponse(product);
    }

    // Creates a new product
    public async Task<Guid> CreateAsync(CreateProductRequest request)
    {
        var product = ProductMappers.MapToProduct(request);
        product.Id = await productRepository.CreateAsync(product);
        return product.Id;
    }

    // Updates an existing product
    public async Task UpdateAsync(UpdateProductRequest request)
    {
        var product = ProductMappers.MapToProduct(request);
        await productRepository.UpdateAsync(product);
    }

    // Deletes a product by ID
    public async Task DeleteAsync(Guid id)
    {
        await productRepository.DeleteAsync(id);
    }

}
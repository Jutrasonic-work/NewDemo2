using Catalog.Application.Features.Products.Contracts.Requests;
using Catalog.Application.Features.Products.Contracts.Responses;

namespace Catalog.Application.Features.Products.Interfaces;
public interface IProductService
{
    /// <summary>
    /// Gets a product by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<GetProductResponse> GetAsync(Guid id);

    /// <summary>
    /// Gets a list of products with optional filtering.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetProductsResponse> GetListAsync(GetProductsRequest request);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Guid> CreateAsync(CreateProductRequest request);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateProductRequest request);

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}

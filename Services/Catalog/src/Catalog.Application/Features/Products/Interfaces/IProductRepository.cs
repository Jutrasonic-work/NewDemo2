using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Products.Interfaces;

public interface IProductRepository
{
    /// <summary>
    /// Gets a list of products with their associated categories.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Product>> GetListAsync();

    /// <summary>
    /// Gets a product by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Product> GetAsync(Guid id);

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task<Guid> CreateAsync(Product product);

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    Task UpdateAsync(Product product);

    /// <summary>
    /// Deletes a product by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}

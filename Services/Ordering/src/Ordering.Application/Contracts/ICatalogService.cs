using Ordering.Domain.Dtos;

namespace Ordering.Application.Contracts;

public interface ICatalogService
{
    Task<ProductDto?> GetProductById(Guid productId);
} 
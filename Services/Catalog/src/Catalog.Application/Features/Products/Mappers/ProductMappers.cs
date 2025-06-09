using Catalog.Application.Features.Products.Contracts.Requests;
using Catalog.Application.Features.Products.Contracts.Responses;
using Catalog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Catalog.Application.Mappers;

[Mapper]
public static partial class ProductMappers
{
    public static partial GetProductResponse MapToResponse(Product product);
    
    public static partial GetProductsItemResponse MapToItemResponse(Product product);

    public static partial IEnumerable<GetProductsItemResponse> MapToResponse(IEnumerable<Product> products);

    public static partial Product MapToProduct(CreateProductRequest request);
    public static partial Product MapToProduct(UpdateProductRequest request);
}

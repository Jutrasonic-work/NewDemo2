using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetCategoryProductsUseCase(ICatalogService catalogService)
{
    public async Task<IEnumerable<ProductResponse>> ExecuteAsync(int categoryId)
    {
        return await catalogService.GetCategoryProductsAsync(categoryId);
    }
}
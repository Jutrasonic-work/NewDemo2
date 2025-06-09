using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetProductsUseCase(ICatalogService catalogService)
{
    public async Task<IEnumerable<ProductResponse>> ExecuteAsync()
    {
        return await catalogService.GetProductsAsync();
    }
}
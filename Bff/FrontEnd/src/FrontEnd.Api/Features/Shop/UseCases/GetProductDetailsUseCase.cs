using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetProductDetailsUseCase(ICatalogService catalogService)
{
    public async Task<ProductResponse?> ExecuteAsync(int id)
    {
        return await catalogService.GetProductAsync(id);
    }
}
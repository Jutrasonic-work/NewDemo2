using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetCategoriesUseCase(ICatalogService catalogService)
{
    public async Task<IEnumerable<CategoryResponse>> ExecuteAsync()
    {
        return await catalogService.GetCategoriesAsync();
    }
}

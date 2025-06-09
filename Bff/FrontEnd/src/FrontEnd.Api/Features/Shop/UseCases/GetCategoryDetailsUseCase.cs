using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetCategoryDetailsUseCase(ICatalogService catalogService)
{
    public async Task<CategoryResponse?> ExecuteAsync(int id)
    {
        return await catalogService.GetCategoryAsync(id);
    }
}

using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class CreateCategoryUseCase(ICatalogService catalogService)
{
    public async Task<int> ExecuteAsync(CreateCategoryRequest request)
    {
        return await catalogService.CreateCategoryAsync(request);
    }
}

using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class UpdateCategoryUseCase(ICatalogService catalogService)
{
    public async Task ExecuteAsync(int id, UpdateCategoryRequest request)
    {
        await catalogService.UpdateCategoryAsync(id, request);
    }
}

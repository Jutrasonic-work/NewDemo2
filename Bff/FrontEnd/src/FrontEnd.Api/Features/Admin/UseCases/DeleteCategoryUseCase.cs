using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class DeleteCategoryUseCase(ICatalogService catalogService)
{
    public async Task ExecuteAsync(int id)
    {
        await catalogService.DeleteCategoryAsync(id);
    }
}
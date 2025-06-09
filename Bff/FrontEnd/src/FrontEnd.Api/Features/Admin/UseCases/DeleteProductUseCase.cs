using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class DeleteProductUseCase(ICatalogService catalogService)
{
    public async Task ExecuteAsync(int id)
    {
        await catalogService.DeleteProductAsync(id);
    }
}
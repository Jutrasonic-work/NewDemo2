using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class UpdateProductUseCase(ICatalogService catalogService)
{
    public async Task ExecuteAsync(int id, UpdateProductRequest request)
    {
        await catalogService.UpdateProductAsync(id, request);
    }
}
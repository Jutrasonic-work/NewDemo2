using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Services.Catalog;

namespace FrontEnd.Api.Features.Admin.UseCases;

public class CreateProductUseCase(ICatalogService catalogService)
{
    public async Task<int> ExecuteAsync(CreateProductRequest request)
    {
        return await catalogService.CreateProductAsync(request);
    }
}
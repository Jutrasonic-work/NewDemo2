using FrontEnd.Api.Features.Admin.UseCases;

namespace FrontEnd.Api.Features.Admin;

public static class DependencyInjection
{
    public static IServiceCollection AddAdminFeatures(this IServiceCollection services)
    {
        services.AddScoped<CreateProductUseCase>();
        services.AddScoped<UpdateProductUseCase>();
        services.AddScoped<DeleteProductUseCase>();
        services.AddScoped<CreateCategoryUseCase>();
        services.AddScoped<UpdateCategoryUseCase>();
        services.AddScoped<DeleteCategoryUseCase>();
        return services;
    }
}

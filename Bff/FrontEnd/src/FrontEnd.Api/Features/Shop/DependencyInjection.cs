using FrontEnd.Api.Features.Shop.UseCases;

namespace FrontEnd.Api.Features.Shop;

public static class DependencyInjection
{
    public static IServiceCollection AddShopFeatures(this IServiceCollection services)
    {
        services.AddScoped<GetProductsUseCase>();
        services.AddScoped<GetProductDetailsUseCase>();
        services.AddScoped<GetOrderUseCase>();
        services.AddScoped<GetCategoriesUseCase>();
        services.AddScoped<GetCategoryDetailsUseCase>();
        services.AddScoped<AddToOrderUseCase>();
        services.AddScoped<RemoveFromOrderUseCase>();
        services.AddScoped<GetOrderUseCase>();
        return services;
    }
}

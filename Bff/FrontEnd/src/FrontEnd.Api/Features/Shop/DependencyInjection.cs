using FrontEnd.Api.Features.Shop.UseCases;

namespace FrontEnd.Api.Features.Shop;

public static class DependencyInjection
{
    public static IServiceCollection AddShopFeatures(this IServiceCollection services)
    {
        services.AddScoped<GetProductsUseCase>();
        services.AddScoped<GetProductDetailsUseCase>();
        services.AddScoped<GetCartUseCase>();
        services.AddScoped<GetCategoriesUseCase>();
        services.AddScoped<GetCategoryDetailsUseCase>();
        services.AddScoped<AddToCartUseCase>();
        services.AddScoped<UpdateCartItemUseCase>();
        services.AddScoped<RemoveFromCartUseCase>();
        services.AddScoped<GetCartUseCase>();
        return services;
    }
}

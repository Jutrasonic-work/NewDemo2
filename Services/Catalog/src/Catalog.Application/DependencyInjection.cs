using Catalog.Application.Features.Categories.Interfaces;
using Catalog.Application.Features.Categories.Services;
using Catalog.Application.Features.Products.Interfaces;
using Catalog.Application.Features.Products.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}

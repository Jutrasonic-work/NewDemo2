using Catalog.Application.Features.Categories.Interfaces;
using Catalog.Application.Features.Products.Interfaces;
using Catalog.Infrastructure.Core.Sql;
using Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddSingleton<ISqlFactory, SqlFactory>();


        return services;
    }
}

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.Application.Contracts;
using Ordering.Application.Services;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Enregistre tous les handlers MediatR
        //services.AddMediatR(cfg => {
        //    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        //});

        // Enregistre les services de l'application
        services.AddHttpClient<ICatalogService, CatalogService>();

        return services;
    }
} 
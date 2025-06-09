using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Enregistrement des services de l'application
        services.AddApplication();

        // Enregistrement des services de l'infrastructure
        services.AddInfrastructure(services.BuildServiceProvider().GetRequiredService<IConfiguration>());

        return services;
    }
} 
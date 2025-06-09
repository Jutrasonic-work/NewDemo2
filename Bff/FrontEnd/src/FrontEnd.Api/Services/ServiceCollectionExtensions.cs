using FrontEnd.Api.Services.Auth;
using FrontEnd.Api.Services.Catalog;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration des clients HTTP
        services.AddHttpClient("CatalogApi", client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:CatalogApi"] ?? throw new InvalidOperationException("URL de l'API Catalog non configurée"));
        });

        services.AddHttpClient("OrderApi", client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:OrderApi"] ?? throw new InvalidOperationException("URL de l'API Order non configurée"));
        });

        services.AddHttpClient("AuthApi", client =>
        {
            client.BaseAddress = new Uri(configuration["ApiSettings:AuthApi"] ?? throw new InvalidOperationException("URL de l'API Auth non configurée"));
        });

        // Enregistrement des services
        services.AddScoped<ICatalogService, CatalogService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
} 
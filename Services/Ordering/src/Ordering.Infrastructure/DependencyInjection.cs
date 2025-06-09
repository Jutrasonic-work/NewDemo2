using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Repositories;
using Ordering.Infrastructure.Repositories;
using System.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuration de la base de données
        services.AddScoped<IDbConnection>(sp => 
            new SqlConnection(configuration.GetConnectionString("Demo")));

        // Enregistrement des repositories
        services.AddScoped<IOrderRepository, OrderRepository>();



        return services;
    }
} 
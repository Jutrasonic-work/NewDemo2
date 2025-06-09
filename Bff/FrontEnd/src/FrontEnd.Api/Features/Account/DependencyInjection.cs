using FrontEnd.Api.Features.Account.UseCases;

namespace FrontEnd.Api.Features.Account;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountFeatures(this IServiceCollection services)
    {
        services.AddScoped<LoginUseCase>();
        return services;
    }
}

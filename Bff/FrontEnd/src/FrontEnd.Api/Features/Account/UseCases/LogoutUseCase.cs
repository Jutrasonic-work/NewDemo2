using FrontEnd.Api.Services.Auth;

namespace FrontEnd.Api.Features.Account.UseCases;

public class LogoutUseCase(IAuthService authService)
{
    public async Task ExecuteAsync()
    {
        await authService.LogoutAsync();
    }
}
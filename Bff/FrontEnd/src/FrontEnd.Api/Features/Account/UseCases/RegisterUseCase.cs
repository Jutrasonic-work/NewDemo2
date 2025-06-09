using FrontEnd.Api.Features.Account.Contracts.Requests;
using FrontEnd.Api.Services.Auth;

namespace FrontEnd.Api.Features.Account.UseCases;

public class RegisterUseCase(IAuthService authService)

{
    public async Task ExecuteAsync(RegisterRequest request)
    {
        await authService.RegisterAsync(request);
    }

}
using FrontEnd.Api.Features.Account.Contracts.Requests;
using FrontEnd.Api.Features.Account.Contracts.Responses;
using FrontEnd.Api.Services.Auth;

namespace FrontEnd.Api.Features.Account.UseCases;

public class LoginUseCase(IAuthService authService)
{
    public async Task<LoginResponse> ExecuteAsync(LoginRequest request)
    {
        return await authService.LoginAsync(request);
    }
}

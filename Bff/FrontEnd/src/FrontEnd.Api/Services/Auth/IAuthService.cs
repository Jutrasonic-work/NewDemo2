using FrontEnd.Api.Features.Account.Contracts.Requests;
using FrontEnd.Api.Features.Account.Contracts.Responses;

namespace FrontEnd.Api.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    Task RegisterAsync(RegisterRequest request);
} 
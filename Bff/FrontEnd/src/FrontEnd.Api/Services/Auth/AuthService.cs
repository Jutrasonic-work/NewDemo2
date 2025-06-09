using FrontEnd.Api.Features.Account.Contracts.Requests;
using FrontEnd.Api.Features.Account.Contracts.Responses;
using System.Net.Http.Json;

namespace FrontEnd.Api.Services.Auth;

public class AuthService(IHttpClientFactory httpClientFactory, ILogger<AuthService> logger) : IAuthService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("AuthApi");

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LoginResponse>()
                ?? throw new InvalidOperationException("Réponse de connexion invalide");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la connexion");
            throw;
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("/api/auth/logout", null);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de la déconnexion");
            throw;
        }
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erreur lors de l'inscription");
            throw;
        }
    }
} 
using Refit;

namespace api_gateway.Services;

/// <summary>
/// Refit интерфейс для вызовов Auth Service
/// </summary>
public interface IAuthServiceClient
{
    [Post("/api/auth/register")]
    Task<IApiResponse<object>> RegisterAsync([Body] object request);

    [Post("/api/auth/login")]
    Task<IApiResponse<object>> LoginAsync([Body] object request);

    [Post("/api/auth/refresh")]
    Task<IApiResponse<object>> RefreshTokenAsync([Body] object request);

    [Post("/api/auth/logout")]
    Task<IApiResponse<object>> LogoutAsync([Body] object request, [Header("Authorization")] string? authorization);

    [Get("/api/account/me")]
    Task<IApiResponse<object>> GetCurrentUserAsync([Header("Authorization")] string? authorization);
}

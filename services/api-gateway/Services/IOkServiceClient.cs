using Refit;

namespace api_gateway.Services;

/// <summary>
/// Refit интерфейс для вызовов OK Service
/// </summary>
public interface IOkServiceClient
{
    [Get("/api/ok/users/{userId}/profile")]
    Task<IApiResponse<object>> GetUserProfileAsync(string userId, [Header("Authorization")] string? authorization);

    [Get("/api/ok/users/{userId}/followers")]
    Task<IApiResponse<object>> GetFollowersAsync(string userId, [Header("Authorization")] string? authorization);

    [Get("/api/ok/users/{userId}/friends")]
    Task<IApiResponse<object>> GetFriendsAsync(string userId, [Header("Authorization")] string? authorization);
}

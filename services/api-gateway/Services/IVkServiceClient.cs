using Refit;

namespace ApiGateway.Services;

/// <summary>
/// Refit интерфейс для вызовов VK Service
/// </summary>
public interface IVkServiceClient
{
    [Get("/api/vk/users/{userId}/profile")]
    Task<IApiResponse<object>> GetUserProfileAsync(string userId, [Header("Authorization")] string? authorization);

    [Get("/api/vk/users/{userId}/followers")]
    Task<IApiResponse<object>> GetFollowersAsync(string userId, [Header("Authorization")] string? authorization);

    [Get("/api/vk/users/{userId}/friends")]
    Task<IApiResponse<object>> GetFriendsAsync(string userId, [Header("Authorization")] string? authorization);
}

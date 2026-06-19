namespace AuthService.Models.Responses;

/// <summary>
/// Модель ответа аутентификации
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// JWT access token
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Refresh token
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Дата истечения access token
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public UserResponse User { get; set; } = null!;
}

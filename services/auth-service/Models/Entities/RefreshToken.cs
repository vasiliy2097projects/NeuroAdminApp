namespace AuthService.Models.Entities;

/// <summary>
/// Сущность refresh token
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Токен (уникальный)
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Дата истечения
    /// </summary>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата отзыва
    /// </summary>
    public DateTime? RevokedAt { get; set; }
}

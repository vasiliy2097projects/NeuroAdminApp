namespace AuthService.Models.Entities;

/// <summary>
/// Сущность токена сброса пароля
/// </summary>
public class PasswordResetToken
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
    /// Дата использования
    /// </summary>
    public DateTime? UsedAt { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

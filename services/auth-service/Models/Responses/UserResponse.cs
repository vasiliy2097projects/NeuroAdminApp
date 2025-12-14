namespace AuthService.Models.Responses;

/// <summary>
/// Модель ответа с информацией о пользователе
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Дата регистрации
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Email верифицирован
    /// </summary>
    public bool EmailVerified { get; set; }
}

namespace AuthService.Models.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Email (уникальный)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хеш пароля (BCrypt)
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Роль (0 = User, 1 = Admin)
    /// </summary>
    public int Role { get; set; }

    /// <summary>
    /// Статус (0 = Active, 1 = Inactive, 2 = Banned)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// Email верифицирован
    /// </summary>
    public bool EmailVerified { get; set; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата обновления
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Дата последнего входа
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
}

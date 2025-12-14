using AuthService.Models.Entities;

namespace AuthService.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с токенами сброса пароля
/// </summary>
public interface IPasswordResetTokenRepository
{
    /// <summary>
    /// Создать токен сброса пароля
    /// </summary>
    Task CreateAsync(PasswordResetToken token);

    /// <summary>
    /// Получить токен по значению
    /// </summary>
    Task<PasswordResetToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Пометить токен как использованный
    /// </summary>
    Task MarkAsUsedAsync(string token);

    /// <summary>
    /// Удалить истекшие токены
    /// </summary>
    Task DeleteExpiredAsync();
}

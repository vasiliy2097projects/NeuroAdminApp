using AuthService.Models.Entities;

namespace AuthService.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с токенами верификации email
/// </summary>
public interface IEmailVerificationTokenRepository
{
    /// <summary>
    /// Создать токен верификации
    /// </summary>
    Task CreateAsync(EmailVerificationToken token);

    /// <summary>
    /// Получить токен по значению
    /// </summary>
    Task<EmailVerificationToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Пометить токен как использованный
    /// </summary>
    Task MarkAsUsedAsync(string token);

    /// <summary>
    /// Удалить истекшие токены
    /// </summary>
    Task DeleteExpiredAsync();
}

using AuthService.Models.Entities;

namespace AuthService.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с refresh tokens
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Создать новый refresh token
    /// </summary>
    Task CreateAsync(RefreshToken token);

    /// <summary>
    /// Получить refresh token по токену
    /// </summary>
    Task<RefreshToken?> GetByTokenAsync(string token);

    /// <summary>
    /// Отозвать refresh token
    /// </summary>
    Task RevokeAsync(string token);

    /// <summary>
    /// Отозвать все refresh tokens пользователя
    /// </summary>
    Task RevokeAllForUserAsync(Guid userId);

    /// <summary>
    /// Удалить истекшие токены
    /// </summary>
    Task DeleteExpiredAsync();
}

using AuthService.Models.Entities;

namespace AuthService.Repositories;

/// <summary>
/// Интерфейс репозитория для работы с пользователями
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Создать нового пользователя
    /// </summary>
    Task<Guid> CreateAsync(User user);

    /// <summary>
    /// Получить пользователя по email
    /// </summary>
    Task<User?> GetByEmailAsync(string email);

    /// <summary>
    /// Получить пользователя по ID
    /// </summary>
    Task<User?> GetByIdAsync(Guid id);

    /// <summary>
    /// Обновить пользователя
    /// </summary>
    Task<bool> UpdateAsync(User user);

    /// <summary>
    /// Проверить существование пользователя с указанным email
    /// </summary>
    Task<bool> ExistsByEmailAsync(string email);
}

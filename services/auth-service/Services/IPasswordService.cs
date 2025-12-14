namespace AuthService.Services;

/// <summary>
/// Интерфейс сервиса для работы с паролями
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Хешировать пароль
    /// </summary>
    string HashPassword(string password);

    /// <summary>
    /// Проверить пароль
    /// </summary>
    bool VerifyPassword(string password, string hashedPassword);
}

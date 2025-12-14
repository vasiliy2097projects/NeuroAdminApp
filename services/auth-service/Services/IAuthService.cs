using AuthService.Models.Entities;
using AuthService.Models.Requests;
using AuthService.Models.Responses;

namespace AuthService.Services;

/// <summary>
/// Интерфейс сервиса аутентификации
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Зарегистрировать нового пользователя
    /// </summary>
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Войти в систему
    /// </summary>
    Task<AuthResponse> LoginAsync(LoginRequest request);

    /// <summary>
    /// Обновить токен
    /// </summary>
    Task<AuthResponse> RefreshTokenAsync(string refreshToken);

    /// <summary>
    /// Выйти из системы
    /// </summary>
    Task LogoutAsync(string refreshToken);

    /// <summary>
    /// Верифицировать email
    /// </summary>
    Task<bool> VerifyEmailAsync(string token);

    /// <summary>
    /// Запросить сброс пароля
    /// </summary>
    Task ForgotPasswordAsync(string email);

    /// <summary>
    /// Сбросить пароль
    /// </summary>
    Task ResetPasswordAsync(ResetPasswordRequest request);
}

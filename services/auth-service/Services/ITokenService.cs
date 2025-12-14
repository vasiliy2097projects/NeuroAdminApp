using System.Security.Claims;

namespace AuthService.Services;

/// <summary>
/// Интерфейс сервиса для работы с JWT токенами
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Генерировать access token
    /// </summary>
    string GenerateAccessToken(Guid userId, string email, string firstName, string lastName);

    /// <summary>
    /// Генерировать refresh token
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Получить claims из токена
    /// </summary>
    ClaimsPrincipal? GetPrincipalFromToken(string token);

    /// <summary>
    /// Валидировать токен
    /// </summary>
    bool ValidateToken(string token);
}

using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса обновления токена
/// </summary>
public class RefreshTokenRequest
{
    /// <summary>
    /// Refresh token
    /// </summary>
    [Required(ErrorMessage = "Refresh token is required")]
    public string RefreshToken { get; set; } = string.Empty;
}

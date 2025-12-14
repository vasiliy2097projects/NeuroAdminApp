using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса сброса пароля
/// </summary>
public class ResetPasswordRequest
{
    /// <summary>
    /// Токен сброса пароля
    /// </summary>
    [Required(ErrorMessage = "Reset token is required")]
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Новый пароль
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Подтверждение нового пароля
    /// </summary>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

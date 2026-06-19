using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса смены пароля
/// </summary>
public class ChangePasswordRequest
{
    /// <summary>
    /// Текущий пароль
    /// </summary>
    [Required(ErrorMessage = "Current password is required")]
    public string CurrentPassword { get; set; } = string.Empty;

    /// <summary>
    /// Новый пароль
    /// </summary>
    [Required(ErrorMessage = "New password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string NewPassword { get; set; } = string.Empty;

    /// <summary>
    /// Подтверждение нового пароля
    /// </summary>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

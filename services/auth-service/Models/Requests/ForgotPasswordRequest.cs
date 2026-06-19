using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса восстановления пароля
/// </summary>
public class ForgotPasswordRequest
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
}

using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса входа в систему
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}

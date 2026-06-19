using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса регистрации пользователя
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Пароль (минимум 8 символов)
    /// </summary>
    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required(ErrorMessage = "First name is required")]
    [MinLength(1, ErrorMessage = "First name cannot be empty")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required(ErrorMessage = "Last name is required")]
    [MinLength(1, ErrorMessage = "Last name cannot be empty")]
    public string LastName { get; set; } = string.Empty;
}

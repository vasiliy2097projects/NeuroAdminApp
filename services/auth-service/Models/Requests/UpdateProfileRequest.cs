using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса обновления профиля
/// </summary>
public class UpdateProfileRequest
{
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

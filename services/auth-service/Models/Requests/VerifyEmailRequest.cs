using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

/// <summary>
/// Модель запроса верификации email
/// </summary>
public class VerifyEmailRequest
{
    /// <summary>
    /// Токен верификации email
    /// </summary>
    [Required(ErrorMessage = "Verification token is required")]
    public string Token { get; set; } = string.Empty;
}

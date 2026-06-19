using AuthService.Logging;
using AuthService.Models.Requests;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

/// <summary>
/// Контроллер для аутентификации
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogRegisterError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Вход в систему
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogLoginError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Обновление токена
    /// </summary>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            var response = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogRefreshTokenError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Выход из системы
    /// </summary>
    [HttpPost("logout")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequest request)
    {
        try
        {
            await _authService.LogoutAsync(request.RefreshToken);
            return Ok(new { message = "Logged out successfully" });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogLogoutError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Верификация email
    /// </summary>
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
    {
        try
        {
            var result = await _authService.VerifyEmailAsync(request.Token);
            if (result)
            {
                return Ok(new { message = "Email verified successfully" });
            }

            return BadRequest(new { error = "Invalid or expired verification token" });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogVerifyEmailError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Запрос сброса пароля
    /// </summary>
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        try
        {
            await _authService.ForgotPasswordAsync(request.Email);
            // Всегда возвращаем успех для безопасности
            return Ok(new { message = "If the email exists, a password reset link has been sent" });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogForgotPasswordError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Сброс пароля
    /// </summary>
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            await _authService.ResetPasswordAsync(request);
            return Ok(new { message = "Password reset successfully" });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogResetPasswordError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}

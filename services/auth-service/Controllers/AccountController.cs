using System.Security.Claims;
using AuthService.Logging;
using AuthService.Models.Requests;
using AuthService.Models.Responses;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

/// <summary>
/// Контроллер для управления аккаунтом
/// </summary>
[ApiController]
[Route("api/account")]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IAuthService authService,
        ILogger<AccountController> logger)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Получить информацию о текущем пользователе
    /// </summary>
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(new { error = "Invalid token" });
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            var response = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                EmailVerified = user.EmailVerified
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogGetCurrentUserError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Обновить профиль пользователя
    /// </summary>
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(new { error = "Invalid token" });
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            var updated = await _userRepository.UpdateAsync(user);
            if (!updated)
            {
                return StatusCode(500, new { error = "Failed to update profile" });
            }

            var response = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                EmailVerified = user.EmailVerified
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogUpdateProfileError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    /// <summary>
    /// Сменить пароль
    /// </summary>
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(new { error = "Invalid token" });
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound(new { error = "User not found" });
            }

            if (!_passwordService.VerifyPassword(request.CurrentPassword, user.PasswordHash))
            {
                return BadRequest(new { error = "Current password is incorrect" });
            }

            user.PasswordHash = _passwordService.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            var updated = await _userRepository.UpdateAsync(user);
            if (!updated)
            {
                return StatusCode(500, new { error = "Failed to change password" });
            }

            return Ok(new { message = "Password changed successfully" });
        }
        catch (Exception ex)
        {
            LoggerDefinitions.LogChangePasswordError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}

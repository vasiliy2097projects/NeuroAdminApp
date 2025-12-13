using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер для проксирования запросов к Auth Service
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthServiceClient _authServiceClient;
    private readonly ILogger<AuthController> _logger;

    private static readonly Action<ILogger, Exception> LogRegisterError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, "RegisterError"),
            "Error calling auth service register");

    private static readonly Action<ILogger, Exception> LogLoginError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2, "LoginError"),
            "Error calling auth service login");

    private static readonly Action<ILogger, Exception> LogRefreshTokenError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3, "RefreshTokenError"),
            "Error calling auth service refresh token");

    private static readonly Action<ILogger, Exception> LogLogoutError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(4, "LogoutError"),
            "Error calling auth service logout");

    private static readonly Action<ILogger, Exception> LogGetCurrentUserError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(5, "GetCurrentUserError"),
            "Error calling auth service get current user");

    public AuthController(IAuthServiceClient authServiceClient, ILogger<AuthController> logger)
    {
        _authServiceClient = authServiceClient;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] object request)
    {
        try
        {
            var response = await _authServiceClient.RegisterAsync(request);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogRegisterError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] object request)
    {
        try
        {
            var response = await _authServiceClient.LoginAsync(request);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogLoginError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] object request)
    {
        try
        {
            var response = await _authServiceClient.RefreshTokenAsync(request);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogRefreshTokenError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpPost("logout")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> Logout([FromBody] object request)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _authServiceClient.LogoutAsync(request, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogLogoutError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("me")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _authServiceClient.GetCurrentUserAsync(token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetCurrentUserError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    private ObjectResult HandleRefitResponse(Refit.IApiResponse<object> response)
    {
        // For error responses, content is in Error.Content, not Content
        var content = response.IsSuccessStatusCode ? response.Content : response.Error?.Content ?? response.Content;
        return StatusCode((int)response.StatusCode, content);
    }
}

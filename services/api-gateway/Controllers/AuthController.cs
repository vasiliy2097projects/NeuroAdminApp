using ApiGateway.Logging;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

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
            LoggerDefinitions.LogRegisterError(_logger, ex);
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
            LoggerDefinitions.LogLoginError(_logger, ex);
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
            LoggerDefinitions.LogRefreshTokenError(_logger, ex);
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
            LoggerDefinitions.LogLogoutError(_logger, ex);
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
            LoggerDefinitions.LogGetCurrentUserError(_logger, ex);
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

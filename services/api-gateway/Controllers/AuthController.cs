using api_gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Controllers;

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
            _logger.LogError(ex, "Error calling auth service register");
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
            _logger.LogError(ex, "Error calling auth service login");
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
            _logger.LogError(ex, "Error calling auth service refresh token");
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
            _logger.LogError(ex, "Error calling auth service logout");
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
            _logger.LogError(ex, "Error calling auth service get current user");
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

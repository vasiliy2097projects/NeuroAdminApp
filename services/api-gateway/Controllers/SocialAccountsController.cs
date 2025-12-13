using ApiGateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер для проксирования запросов к VK и OK Services
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SocialAccountsController : ControllerBase
{
    private readonly IVkServiceClient _vkServiceClient;
    private readonly IOkServiceClient _okServiceClient;
    private readonly ILogger<SocialAccountsController> _logger;

    private static readonly Action<ILogger, Exception> LogGetVkUserProfileError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, "GetVkUserProfileError"),
            "Error calling VK service get user profile");

    private static readonly Action<ILogger, Exception> LogGetVkFollowersError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2, "GetVkFollowersError"),
            "Error calling VK service get followers");

    private static readonly Action<ILogger, Exception> LogGetOkUserProfileError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3, "GetOkUserProfileError"),
            "Error calling OK service get user profile");

    private static readonly Action<ILogger, Exception> LogGetOkFollowersError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(4, "GetOkFollowersError"),
            "Error calling OK service get followers");

    public SocialAccountsController(
        IVkServiceClient vkServiceClient,
        IOkServiceClient okServiceClient,
        ILogger<SocialAccountsController> logger)
    {
        _vkServiceClient = vkServiceClient;
        _okServiceClient = okServiceClient;
        _logger = logger;
    }

    [HttpGet("vk/users/{userId}/profile")]
    public async Task<IActionResult> GetVkUserProfile(string userId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _vkServiceClient.GetUserProfileAsync(userId, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetVkUserProfileError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("vk/users/{userId}/followers")]
    public async Task<IActionResult> GetVkFollowers(string userId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _vkServiceClient.GetFollowersAsync(userId, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetVkFollowersError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("ok/users/{userId}/profile")]
    public async Task<IActionResult> GetOkUserProfile(string userId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _okServiceClient.GetUserProfileAsync(userId, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetOkUserProfileError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("ok/users/{userId}/followers")]
    public async Task<IActionResult> GetOkFollowers(string userId)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _okServiceClient.GetFollowersAsync(userId, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetOkFollowersError(_logger, ex);
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

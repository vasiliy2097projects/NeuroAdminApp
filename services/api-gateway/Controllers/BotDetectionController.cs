using ApiGateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер для проксирования запросов к Bot Detection Service
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BotDetectionController : ControllerBase
{
    private readonly IBotDetectionServiceClient _botDetectionServiceClient;
    private readonly ILogger<BotDetectionController> _logger;

    private static readonly Action<ILogger, Exception> LogStartAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1, "StartAnalysisError"),
            "Error calling bot detection service start analysis");

    private static readonly Action<ILogger, Exception> LogGetAnalysesError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2, "GetAnalysesError"),
            "Error calling bot detection service get analyses");

    private static readonly Action<ILogger, Exception> LogGetAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3, "GetAnalysisError"),
            "Error calling bot detection service get analysis");

    private static readonly Action<ILogger, Exception> LogGetAnalysisResultsError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(4, "GetAnalysisResultsError"),
            "Error calling bot detection service get analysis results");

    private static readonly Action<ILogger, Exception> LogDeleteAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(5, "DeleteAnalysisError"),
            "Error calling bot detection service delete analysis");

    public BotDetectionController(IBotDetectionServiceClient botDetectionServiceClient, ILogger<BotDetectionController> logger)
    {
        _botDetectionServiceClient = botDetectionServiceClient;
        _logger = logger;
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> StartAnalysis([FromBody] object request)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _botDetectionServiceClient.StartAnalysisAsync(request, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogStartAnalysisError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("analyses")]
    public async Task<IActionResult> GetAnalyses()
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _botDetectionServiceClient.GetAnalysesAsync(token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetAnalysesError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("analyses/{id}")]
    public async Task<IActionResult> GetAnalysis(Guid id)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _botDetectionServiceClient.GetAnalysisAsync(id, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetAnalysisError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpGet("analyses/{id}/results")]
    public async Task<IActionResult> GetAnalysisResults(Guid id)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _botDetectionServiceClient.GetAnalysisResultsAsync(id, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogGetAnalysisResultsError(_logger, ex);
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    [HttpDelete("analyses/{id}")]
    public async Task<IActionResult> DeleteAnalysis(Guid id)
    {
        try
        {
            var token = Request.Headers["Authorization"].ToString();
            var response = await _botDetectionServiceClient.DeleteAnalysisAsync(id, token);
            return HandleRefitResponse(response);
        }
        catch (Exception ex)
        {
            LogDeleteAnalysisError(_logger, ex);
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

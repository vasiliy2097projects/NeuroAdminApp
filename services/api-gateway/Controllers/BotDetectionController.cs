using api_gateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api_gateway.Controllers;

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
            _logger.LogError(ex, "Error calling bot detection service start analysis");
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
            _logger.LogError(ex, "Error calling bot detection service get analyses");
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
            _logger.LogError(ex, "Error calling bot detection service get analysis");
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
            _logger.LogError(ex, "Error calling bot detection service get analysis results");
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
            _logger.LogError(ex, "Error calling bot detection service delete analysis");
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

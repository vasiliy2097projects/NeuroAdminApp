using Refit;

namespace api_gateway.Services;

/// <summary>
/// Refit интерфейс для вызовов Bot Detection Service
/// </summary>
public interface IBotDetectionServiceClient
{
    [Post("/api/bot-detection/analyze")]
    Task<IApiResponse<object>> StartAnalysisAsync([Body] object request, [Header("Authorization")] string? authorization);

    [Get("/api/bot-detection/analyses")]
    Task<IApiResponse<object>> GetAnalysesAsync([Header("Authorization")] string? authorization);

    [Get("/api/bot-detection/analyses/{id}")]
    Task<IApiResponse<object>> GetAnalysisAsync(Guid id, [Header("Authorization")] string? authorization);

    [Get("/api/bot-detection/analyses/{id}/results")]
    Task<IApiResponse<object>> GetAnalysisResultsAsync(Guid id, [Header("Authorization")] string? authorization);

    [Delete("/api/bot-detection/analyses/{id}")]
    Task<IApiResponse<object>> DeleteAnalysisAsync(Guid id, [Header("Authorization")] string? authorization);
}

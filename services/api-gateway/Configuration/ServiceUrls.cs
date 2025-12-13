namespace api_gateway.Configuration;

/// <summary>
/// Конфигурация URLs микросервисов
/// </summary>
public class ServiceUrls
{
    public string AuthServiceUrl { get; set; } = string.Empty;
    public string BotDetectionServiceUrl { get; set; } = string.Empty;
    public string VkServiceUrl { get; set; } = string.Empty;
    public string OkServiceUrl { get; set; } = string.Empty;
}

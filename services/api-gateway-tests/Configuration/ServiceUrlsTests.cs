using api_gateway.Configuration;
using FluentAssertions;
using Xunit;

namespace api_gateway.Tests.Configuration;

public class ServiceUrlsTests
{
    [Fact]
    public void ServiceUrls_ShouldHaveDefaultEmptyValues()
    {
        // Arrange & Act
        var serviceUrls = new ServiceUrls();

        // Assert
        serviceUrls.AuthServiceUrl.Should().BeEmpty();
        serviceUrls.BotDetectionServiceUrl.Should().BeEmpty();
        serviceUrls.VkServiceUrl.Should().BeEmpty();
        serviceUrls.OkServiceUrl.Should().BeEmpty();
    }

    [Fact]
    public void ServiceUrls_ShouldAllowSettingValues()
    {
        // Arrange
        var serviceUrls = new ServiceUrls
        {
            AuthServiceUrl = "http://localhost:5001",
            BotDetectionServiceUrl = "http://localhost:5002",
            VkServiceUrl = "http://localhost:5003",
            OkServiceUrl = "http://localhost:5004"
        };

        // Assert
        serviceUrls.AuthServiceUrl.Should().Be("http://localhost:5001");
        serviceUrls.BotDetectionServiceUrl.Should().Be("http://localhost:5002");
        serviceUrls.VkServiceUrl.Should().Be("http://localhost:5003");
        serviceUrls.OkServiceUrl.Should().Be("http://localhost:5004");
    }
}

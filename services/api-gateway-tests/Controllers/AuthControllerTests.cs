using ApiGateway.Controllers;
using ApiGateway.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Refit;
using System.Net;
using Xunit;

namespace ApiGateway.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAuthServiceClient> _authServiceClientMock;
    private readonly Mock<ILogger<AuthController>> _loggerMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _authServiceClientMock = new Mock<IAuthServiceClient>();
        _loggerMock = new Mock<ILogger<AuthController>>();
        _controller = new AuthController(_authServiceClientMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Register_WhenServiceReturnsSuccess_ShouldReturnOk()
    {
        // Arrange
        var request = new { Email = "test@example.com", Password = "password123" };
        var responseContent = new { Token = "test-token", UserId = Guid.NewGuid() };
        var apiResponse = CreateSuccessResponse(responseContent, HttpStatusCode.OK);

        _authServiceClientMock
            .Setup(x => x.RegisterAsync(It.IsAny<object>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _controller.Register(request);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Register_WhenServiceReturnsError_ShouldReturnErrorStatusCode()
    {
        // Arrange
        var request = new { Email = "test@example.com", Password = "password123" };
        var apiResponse = CreateErrorResponse(HttpStatusCode.BadRequest);

        _authServiceClientMock
            .Setup(x => x.RegisterAsync(It.IsAny<object>()))
            .ReturnsAsync(apiResponse);

        // Act
        var result = await _controller.Register(request);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Login_WhenServiceThrowsException_ShouldReturnInternalServerError()
    {
        // Arrange
        var request = new { Email = "test@example.com", Password = "password123" };

        _authServiceClientMock
            .Setup(x => x.LoginAsync(It.IsAny<object>()))
            .ThrowsAsync(new HttpRequestException("Service unavailable"));

        // Act
        var result = await _controller.Login(request);

        // Assert
        result.Should().BeOfType<ObjectResult>();
        var objectResult = result as ObjectResult;
        objectResult!.StatusCode.Should().Be(500);
    }

    [Fact]
    public async Task Logout_WhenTokenProvided_ShouldPassTokenToService()
    {
        // Arrange
        var request = new { };
        var token = "Bearer test-token";
        var apiResponse = CreateSuccessResponse(new { }, HttpStatusCode.OK);

        _authServiceClientMock
            .Setup(x => x.LogoutAsync(It.IsAny<object>(), It.IsAny<string>()))
            .ReturnsAsync(apiResponse);

        _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
        _controller.Request.Headers["Authorization"] = token;

        // Act
        var result = await _controller.Logout(request);

        // Assert
        _authServiceClientMock.Verify(
            x => x.LogoutAsync(It.IsAny<object>(), token),
            Times.Once);
    }

    private IApiResponse<object> CreateSuccessResponse(object content, HttpStatusCode statusCode)
    {
        var response = new Mock<IApiResponse<object>>();
        response.Setup(x => x.StatusCode).Returns(statusCode);
        response.Setup(x => x.IsSuccessStatusCode).Returns(true);
        response.Setup(x => x.Content).Returns(content);
        response.Setup(x => x.Error).Returns((ApiException?)null);
        return response.Object;
    }

    private IApiResponse<object> CreateErrorResponse(HttpStatusCode statusCode)
    {
        var response = new Mock<IApiResponse<object>>();
        // Create ApiException using the simplest constructor
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://test.com");
        var httpResponseMessage = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent("{\"error\":\"Test error\"}")
        };
        // Use the protected constructor via reflection or create a derived class
        // For simplicity, we'll just mock the Error property to return null
        // and test that the status code is correctly returned
        var errorMock = new Mock<ApiException>(requestMessage, null!, httpResponseMessage, null!) { CallBase = false };

        response.Setup(x => x.StatusCode).Returns(statusCode);
        response.Setup(x => x.IsSuccessStatusCode).Returns(false);
        response.Setup(x => x.Content).Returns((object?)null);
        // For testing purposes, we'll set Error to null and test status code handling
        response.Setup(x => x.Error).Returns((ApiException?)null);
        return response.Object;
    }
}

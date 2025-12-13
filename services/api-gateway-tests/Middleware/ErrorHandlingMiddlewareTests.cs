using System.Net;
using System.Text;
using System.Text.Json;
using api_gateway.Middleware;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace api_gateway.Tests.Middleware;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddlewareTests()
    {
        _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        _next = context => Task.CompletedTask;
    }

    [Fact]
    public async Task InvokeAsync_WhenNoException_ShouldPassThrough()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var middleware = new ErrorHandlingMiddleware(_next, _loggerMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task InvokeAsync_WhenExceptionOccurs_ShouldReturnInternalServerError()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        var exception = new Exception("Test exception");
        var failingNext = new RequestDelegate(_ => throw exception);
        var middleware = new ErrorHandlingMiddleware(failingNext, _loggerMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        context.Response.ContentType.Should().Be("application/json");
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();
        responseBody.Should().Contain("error");
    }

    [Fact]
    public async Task InvokeAsync_WhenHttpRequestException_ShouldReturnBadGateway()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        var exception = new HttpRequestException("Service unavailable");
        var failingNext = new RequestDelegate(_ => throw exception);
        var middleware = new ErrorHandlingMiddleware(failingNext, _loggerMock.Object);

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.StatusCode.Should().Be((int)HttpStatusCode.BadGateway);
        context.Response.ContentType.Should().Be("application/json");
        
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();
        responseBody.Should().Contain("Failed to communicate with downstream service");
    }
}

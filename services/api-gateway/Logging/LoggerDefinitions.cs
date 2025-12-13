using Microsoft.Extensions.Logging;

namespace ApiGateway.Logging;

/// <summary>
/// Централизованные определения LoggerMessage делегатов для всего приложения
/// </summary>
public static class LoggerDefinitions
{
    // Auth Controller
    public static readonly Action<ILogger, Exception> LogRegisterError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1001, "RegisterError"),
            "Error calling auth service register");

    public static readonly Action<ILogger, Exception> LogLoginError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1002, "LoginError"),
            "Error calling auth service login");

    public static readonly Action<ILogger, Exception> LogRefreshTokenError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1003, "RefreshTokenError"),
            "Error calling auth service refresh token");

    public static readonly Action<ILogger, Exception> LogLogoutError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1004, "LogoutError"),
            "Error calling auth service logout");

    public static readonly Action<ILogger, Exception> LogGetCurrentUserError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1005, "GetCurrentUserError"),
            "Error calling auth service get current user");

    // Bot Detection Controller
    public static readonly Action<ILogger, Exception> LogStartAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2001, "StartAnalysisError"),
            "Error calling bot detection service start analysis");

    public static readonly Action<ILogger, Exception> LogGetAnalysesError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2002, "GetAnalysesError"),
            "Error calling bot detection service get analyses");

    public static readonly Action<ILogger, Exception> LogGetAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2003, "GetAnalysisError"),
            "Error calling bot detection service get analysis");

    public static readonly Action<ILogger, Exception> LogGetAnalysisResultsError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2004, "GetAnalysisResultsError"),
            "Error calling bot detection service get analysis results");

    public static readonly Action<ILogger, Exception> LogDeleteAnalysisError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(2005, "DeleteAnalysisError"),
            "Error calling bot detection service delete analysis");

    // Social Accounts Controller
    public static readonly Action<ILogger, Exception> LogGetVkUserProfileError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3001, "GetVkUserProfileError"),
            "Error calling VK service get user profile");

    public static readonly Action<ILogger, Exception> LogGetVkFollowersError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3002, "GetVkFollowersError"),
            "Error calling VK service get followers");

    public static readonly Action<ILogger, Exception> LogGetOkUserProfileError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3003, "GetOkUserProfileError"),
            "Error calling OK service get user profile");

    public static readonly Action<ILogger, Exception> LogGetOkFollowersError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(3004, "GetOkFollowersError"),
            "Error calling OK service get followers");

    // Middleware
    public static readonly Action<ILogger, Exception> LogUnhandledException =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(9001, "UnhandledException"),
            "An unhandled exception occurred");
}

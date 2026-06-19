using Microsoft.Extensions.Logging;

namespace AuthService.Logging;

/// <summary>
/// Централизованные определения LoggerMessage делегатов для Auth Service
/// EventId диапазон: 1000-1999
/// </summary>
public static class LoggerDefinitions
{
    // Auth Controller
    public static readonly Action<ILogger, Exception> LogRegisterError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1001, "RegisterError"),
            "Error occurred during user registration");

    public static readonly Action<ILogger, Exception> LogLoginError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1002, "LoginError"),
            "Error occurred during user login");

    public static readonly Action<ILogger, Exception> LogRefreshTokenError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1003, "RefreshTokenError"),
            "Error occurred during token refresh");

    public static readonly Action<ILogger, Exception> LogLogoutError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1004, "LogoutError"),
            "Error occurred during logout");

    public static readonly Action<ILogger, Exception> LogVerifyEmailError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1005, "VerifyEmailError"),
            "Error occurred during email verification");

    public static readonly Action<ILogger, Exception> LogForgotPasswordError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1006, "ForgotPasswordError"),
            "Error occurred during forgot password request");

    public static readonly Action<ILogger, Exception> LogResetPasswordError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1007, "ResetPasswordError"),
            "Error occurred during password reset");

    // Account Controller
    public static readonly Action<ILogger, Exception> LogGetCurrentUserError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1101, "GetCurrentUserError"),
            "Error occurred while getting current user");

    public static readonly Action<ILogger, Exception> LogUpdateProfileError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1102, "UpdateProfileError"),
            "Error occurred during profile update");

    public static readonly Action<ILogger, Exception> LogChangePasswordError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1103, "ChangePasswordError"),
            "Error occurred during password change");

    // Auth Service
    public static readonly Action<ILogger, Exception> LogUserNotFoundError =
        LoggerMessage.Define(
            LogLevel.Warning,
            new EventId(1201, "UserNotFound"),
            "User not found");

    public static readonly Action<ILogger, Exception> LogInvalidCredentialsError =
        LoggerMessage.Define(
            LogLevel.Warning,
            new EventId(1202, "InvalidCredentials"),
            "Invalid credentials provided");

    public static readonly Action<ILogger, Exception> LogEmailAlreadyExistsError =
        LoggerMessage.Define(
            LogLevel.Warning,
            new EventId(1203, "EmailAlreadyExists"),
            "Email already exists");

    public static readonly Action<ILogger, Exception> LogInvalidTokenError =
        LoggerMessage.Define(
            LogLevel.Warning,
            new EventId(1204, "InvalidToken"),
            "Invalid token provided");

    // Repository
    public static readonly Action<ILogger, Exception> LogDatabaseError =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1301, "DatabaseError"),
            "Database error occurred");

    // Middleware
    public static readonly Action<ILogger, Exception> LogUnhandledException =
        LoggerMessage.Define(
            LogLevel.Error,
            new EventId(1901, "UnhandledException"),
            "An unhandled exception occurred");
}

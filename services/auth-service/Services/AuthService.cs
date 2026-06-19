using AuthService.Models.Entities;
using AuthService.Models.Requests;
using AuthService.Models.Responses;
using AuthService.Repositories;
using AuthService.Services;

namespace AuthService.Services;

/// <summary>
/// Сервис аутентификации
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IEmailVerificationTokenRepository _emailVerificationTokenRepository;
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordService _passwordService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IEmailVerificationTokenRepository emailVerificationTokenRepository,
        IPasswordResetTokenRepository passwordResetTokenRepository,
        ITokenService tokenService,
        IPasswordService passwordService,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _emailVerificationTokenRepository = emailVerificationTokenRepository;
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _tokenService = tokenService;
        _passwordService = passwordService;
        _logger = logger;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Проверка существования пользователя
        var exists = await _userRepository.ExistsByEmailAsync(request.Email);
        if (exists)
        {
            throw new InvalidOperationException("Email already registered");
        }

        // Создание пользователя
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email.ToLowerInvariant(),
            PasswordHash = _passwordService.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = 0, // User
            Status = 0, // Active
            EmailVerified = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var userId = await _userRepository.CreateAsync(user);

        // Генерация токенов
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.FirstName, user.LastName);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Сохранение refresh token
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };

        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        // Генерация токена верификации email
        var emailVerificationToken = _tokenService.GenerateRefreshToken();
        var emailTokenEntity = new EmailVerificationToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = emailVerificationToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        await _emailVerificationTokenRepository.CreateAsync(emailTokenEntity);

        // TODO: Отправка email с токеном верификации

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                EmailVerified = user.EmailVerified
            }
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email.ToLowerInvariant());
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        if (user.Status != 0) // Not Active
        {
            throw new UnauthorizedAccessException("Account is not active");
        }

        // Обновление времени последнего входа
        user.LastLoginAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        // Генерация токенов
        var accessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.FirstName, user.LastName);
        var refreshToken = _tokenService.GenerateRefreshToken();

        // Сохранение refresh token
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };

        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                EmailVerified = user.EmailVerified
            }
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
        if (tokenEntity == null || tokenEntity.RevokedAt != null || tokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        var user = await _userRepository.GetByIdAsync(tokenEntity.UserId);
        if (user == null || user.Status != 0)
        {
            throw new UnauthorizedAccessException("User not found or inactive");
        }

        // Отзыв старого токена
        await _refreshTokenRepository.RevokeAsync(refreshToken);

        // Генерация новых токенов
        var newAccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email, user.FirstName, user.LastName);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        // Сохранение нового refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };

        await _refreshTokenRepository.CreateAsync(newRefreshTokenEntity);

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                EmailVerified = user.EmailVerified
            }
        };
    }

    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshTokenRepository.RevokeAsync(refreshToken);
    }

    public async Task<bool> VerifyEmailAsync(string token)
    {
        var tokenEntity = await _emailVerificationTokenRepository.GetByTokenAsync(token);
        if (tokenEntity == null || tokenEntity.UsedAt != null || tokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            return false;
        }

        var user = await _userRepository.GetByIdAsync(tokenEntity.UserId);
        if (user == null)
        {
            return false;
        }

        // Помечаем токен как использованный
        await _emailVerificationTokenRepository.MarkAsUsedAsync(token);

        // Обновляем пользователя
        user.EmailVerified = true;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email.ToLowerInvariant());
        if (user == null)
        {
            // Не раскрываем информацию о существовании пользователя
            return;
        }

        // Генерация токена сброса пароля
        var resetToken = _tokenService.GenerateRefreshToken();
        var resetTokenEntity = new PasswordResetToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = resetToken,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            CreatedAt = DateTime.UtcNow
        };

        await _passwordResetTokenRepository.CreateAsync(resetTokenEntity);

        // TODO: Отправка email с токеном сброса пароля
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var tokenEntity = await _passwordResetTokenRepository.GetByTokenAsync(request.Token);
        if (tokenEntity == null || tokenEntity.UsedAt != null || tokenEntity.ExpiresAt < DateTime.UtcNow)
        {
            throw new InvalidOperationException("Invalid or expired reset token");
        }

        var user = await _userRepository.GetByIdAsync(tokenEntity.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found");
        }

        // Помечаем токен как использованный
        await _passwordResetTokenRepository.MarkAsUsedAsync(request.Token);

        // Обновляем пароль
        user.PasswordHash = _passwordService.HashPassword(request.Password);
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        // Отзываем все refresh tokens пользователя
        await _refreshTokenRepository.RevokeAllForUserAsync(user.Id);
    }
}

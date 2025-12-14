using AuthService.Models.Entities;
using Dapper;
using Npgsql;

namespace AuthService.Repositories;

/// <summary>
/// Репозиторий для работы с токенами сброса пароля
/// </summary>
public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly string _connectionString;
    private readonly ILogger<PasswordResetTokenRepository> _logger;

    public PasswordResetTokenRepository(IConfiguration configuration, ILogger<PasswordResetTokenRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured");
        _logger = logger;
    }

    public async Task CreateAsync(PasswordResetToken token)
    {
        const string sql = @"
            INSERT INTO password_reset_tokens (id, user_id, token, expires_at, used_at, created_at)
            VALUES (@Id, @UserId, @Token, @ExpiresAt, @UsedAt, @CreatedAt);";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, token);
    }

    public async Task<PasswordResetToken?> GetByTokenAsync(string token)
    {
        const string sql = @"
            SELECT id, user_id, token, expires_at, used_at, created_at
            FROM password_reset_tokens
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var resetToken = await connection.QueryFirstOrDefaultAsync<PasswordResetToken>(sql, new { Token = token });
        return resetToken;
    }

    public async Task MarkAsUsedAsync(string token)
    {
        const string sql = @"
            UPDATE password_reset_tokens
            SET used_at = @UsedAt
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Token = token, UsedAt = DateTime.UtcNow });
    }

    public async Task DeleteExpiredAsync()
    {
        const string sql = @"
            DELETE FROM password_reset_tokens
            WHERE expires_at < @Now;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Now = DateTime.UtcNow });
    }
}

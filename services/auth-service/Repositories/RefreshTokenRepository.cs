using AuthService.Models.Entities;
using Dapper;
using Npgsql;

namespace AuthService.Repositories;

/// <summary>
/// Репозиторий для работы с refresh tokens
/// </summary>
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly string _connectionString;
    private readonly ILogger<RefreshTokenRepository> _logger;

    public RefreshTokenRepository(IConfiguration configuration, ILogger<RefreshTokenRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured");
        _logger = logger;
    }

    public async Task CreateAsync(RefreshToken token)
    {
        const string sql = @"
            INSERT INTO refresh_tokens (id, user_id, token, expires_at, created_at, revoked_at)
            VALUES (@Id, @UserId, @Token, @ExpiresAt, @CreatedAt, @RevokedAt);";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, token);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        const string sql = @"
            SELECT id, user_id, token, expires_at, created_at, revoked_at
            FROM refresh_tokens
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var refreshToken = await connection.QueryFirstOrDefaultAsync<RefreshToken>(sql, new { Token = token });
        return refreshToken;
    }

    public async Task RevokeAsync(string token)
    {
        const string sql = @"
            UPDATE refresh_tokens
            SET revoked_at = @RevokedAt
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Token = token, RevokedAt = DateTime.UtcNow });
    }

    public async Task RevokeAllForUserAsync(Guid userId)
    {
        const string sql = @"
            UPDATE refresh_tokens
            SET revoked_at = @RevokedAt
            WHERE user_id = @UserId AND revoked_at IS NULL;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { UserId = userId, RevokedAt = DateTime.UtcNow });
    }

    public async Task DeleteExpiredAsync()
    {
        const string sql = @"
            DELETE FROM refresh_tokens
            WHERE expires_at < @Now;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Now = DateTime.UtcNow });
    }
}

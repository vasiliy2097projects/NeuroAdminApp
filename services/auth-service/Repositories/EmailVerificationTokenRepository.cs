using AuthService.Models.Entities;
using Dapper;
using Npgsql;

namespace AuthService.Repositories;

/// <summary>
/// Репозиторий для работы с токенами верификации email
/// </summary>
public class EmailVerificationTokenRepository : IEmailVerificationTokenRepository
{
    private readonly string _connectionString;
    private readonly ILogger<EmailVerificationTokenRepository> _logger;

    public EmailVerificationTokenRepository(IConfiguration configuration, ILogger<EmailVerificationTokenRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured");
        _logger = logger;
    }

    public async Task CreateAsync(EmailVerificationToken token)
    {
        const string sql = @"
            INSERT INTO email_verification_tokens (id, user_id, token, expires_at, used_at, created_at)
            VALUES (@Id, @UserId, @Token, @ExpiresAt, @UsedAt, @CreatedAt);";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, token);
    }

    public async Task<EmailVerificationToken?> GetByTokenAsync(string token)
    {
        const string sql = @"
            SELECT id, user_id, token, expires_at, used_at, created_at
            FROM email_verification_tokens
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var verificationToken = await connection.QueryFirstOrDefaultAsync<EmailVerificationToken>(sql, new { Token = token });
        return verificationToken;
    }

    public async Task MarkAsUsedAsync(string token)
    {
        const string sql = @"
            UPDATE email_verification_tokens
            SET used_at = @UsedAt
            WHERE token = @Token;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Token = token, UsedAt = DateTime.UtcNow });
    }

    public async Task DeleteExpiredAsync()
    {
        const string sql = @"
            DELETE FROM email_verification_tokens
            WHERE expires_at < @Now;";

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.ExecuteAsync(sql, new { Now = DateTime.UtcNow });
    }
}

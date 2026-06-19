using System.Data;
using AuthService.Models.Entities;
using Dapper;
using Npgsql;

namespace AuthService.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection is not configured");
        _logger = logger;
    }

    public async Task<Guid> CreateAsync(User user)
    {
        const string sql = @"
            INSERT INTO users (id, email, password_hash, first_name, last_name, role, status, email_verified, created_at, updated_at)
            VALUES (@Id, @Email, @PasswordHash, @FirstName, @LastName, @Role, @Status, @EmailVerified, @CreatedAt, @UpdatedAt)
            RETURNING id;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var userId = await connection.QuerySingleAsync<Guid>(sql, user);
        return userId;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        const string sql = @"
            SELECT id, email, password_hash, first_name, last_name, role, status, email_verified, 
                   created_at, updated_at, last_login_at
            FROM users
            WHERE email = @Email;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT id, email, password_hash, first_name, last_name, role, status, email_verified, 
                   created_at, updated_at, last_login_at
            FROM users
            WHERE id = @Id;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var user = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        return user;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        const string sql = @"
            UPDATE users
            SET email = @Email, password_hash = @PasswordHash, first_name = @FirstName, 
                last_name = @LastName, role = @Role, status = @Status, email_verified = @EmailVerified,
                updated_at = @UpdatedAt, last_login_at = @LastLoginAt
            WHERE id = @Id;";

        await using var connection = new NpgsqlConnection(_connectionString);
        var rowsAffected = await connection.ExecuteAsync(sql, user);
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        const string sql = @"
            SELECT EXISTS(SELECT 1 FROM users WHERE email = @Email);";

        await using var connection = new NpgsqlConnection(_connectionString);
        var exists = await connection.QuerySingleAsync<bool>(sql, new { Email = email });
        return exists;
    }
}

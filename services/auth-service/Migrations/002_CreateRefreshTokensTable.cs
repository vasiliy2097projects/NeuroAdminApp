using FluentMigrator;

namespace AuthService.Migrations;

/// <summary>
/// Миграция для создания таблицы refresh_tokens
/// </summary>
[Migration(20241213002)]
public class CreateRefreshTokensTable : Migration
{
    public override void Up()
    {
        Create.Table("refresh_tokens")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("token").AsString(500).NotNullable().Unique()
            .WithColumn("expires_at").AsDateTime().NotNullable()
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("revoked_at").AsDateTime().Nullable();

        Create.ForeignKey("fk_refresh_tokens_user_id")
            .FromTable("refresh_tokens").ForeignColumn("user_id")
            .ToTable("users").PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Index("ix_refresh_tokens_user_id")
            .OnTable("refresh_tokens")
            .OnColumn("user_id");

        Create.Index("ix_refresh_tokens_token")
            .OnTable("refresh_tokens")
            .OnColumn("token");
    }

    public override void Down()
    {
        Delete.Table("refresh_tokens");
    }
}

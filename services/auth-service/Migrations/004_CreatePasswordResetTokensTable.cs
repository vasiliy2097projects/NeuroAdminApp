using FluentMigrator;

namespace AuthService.Migrations;

/// <summary>
/// Миграция для создания таблицы password_reset_tokens
/// </summary>
[Migration(20241213004)]
public class CreatePasswordResetTokensTable : Migration
{
    public override void Up()
    {
        Create.Table("password_reset_tokens")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("token").AsString(500).NotNullable().Unique()
            .WithColumn("expires_at").AsDateTime().NotNullable()
            .WithColumn("used_at").AsDateTime().Nullable()
            .WithColumn("created_at").AsDateTime().NotNullable();

        Create.ForeignKey("fk_password_reset_tokens_user_id")
            .FromTable("password_reset_tokens").ForeignColumn("user_id")
            .ToTable("users").PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Index("ix_password_reset_tokens_user_id")
            .OnTable("password_reset_tokens")
            .OnColumn("user_id");

        Create.Index("ix_password_reset_tokens_token")
            .OnTable("password_reset_tokens")
            .OnColumn("token");
    }

    public override void Down()
    {
        Delete.Table("password_reset_tokens");
    }
}

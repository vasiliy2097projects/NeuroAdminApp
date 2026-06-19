using FluentMigrator;

namespace AuthService.Migrations;

/// <summary>
/// Миграция для создания таблицы email_verification_tokens
/// </summary>
[Migration(20241213003)]
public class CreateEmailVerificationTokensTable : Migration
{
    public override void Up()
    {
        Create.Table("email_verification_tokens")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("user_id").AsGuid().NotNullable()
            .WithColumn("token").AsString(500).NotNullable().Unique()
            .WithColumn("expires_at").AsDateTime().NotNullable()
            .WithColumn("used_at").AsDateTime().Nullable()
            .WithColumn("created_at").AsDateTime().NotNullable();

        Create.ForeignKey("fk_email_verification_tokens_user_id")
            .FromTable("email_verification_tokens").ForeignColumn("user_id")
            .ToTable("users").PrimaryColumn("id")
            .OnDelete(System.Data.Rule.Cascade);

        Create.Index("ix_email_verification_tokens_user_id")
            .OnTable("email_verification_tokens")
            .OnColumn("user_id");

        Create.Index("ix_email_verification_tokens_token")
            .OnTable("email_verification_tokens")
            .OnColumn("token");
    }

    public override void Down()
    {
        Delete.Table("email_verification_tokens");
    }
}

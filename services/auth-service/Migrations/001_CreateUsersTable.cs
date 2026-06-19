using FluentMigrator;

namespace AuthService.Migrations;

/// <summary>
/// Миграция для создания таблицы users
/// </summary>
[Migration(20241213001)]
public class CreateUsersTable : Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("email").AsString(255).NotNullable().Unique()
            .WithColumn("password_hash").AsString(255).NotNullable()
            .WithColumn("first_name").AsString(100).NotNullable()
            .WithColumn("last_name").AsString(100).NotNullable()
            .WithColumn("role").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("status").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("email_verified").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("created_at").AsDateTime().NotNullable()
            .WithColumn("updated_at").AsDateTime().NotNullable()
            .WithColumn("last_login_at").AsDateTime().Nullable();

        Create.Index("ix_users_email")
            .OnTable("users")
            .OnColumn("email");
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureTeamSimulator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addkeystouser3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncryptionIV",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EncryptionKey",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptionIV",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EncryptionKey",
                table: "Users");
        }
    }
}

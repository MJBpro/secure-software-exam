using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecureTeamSimulator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newuserfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Users");
        }
    }
}

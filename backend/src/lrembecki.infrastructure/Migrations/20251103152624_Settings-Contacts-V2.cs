using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsContactsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                schema: "app",
                table: "ContactEntity");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                schema: "app",
                table: "ContactEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                schema: "app",
                table: "ContactEntity");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "app",
                table: "ContactEntity",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosAboutUs202602091304 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FooterDescription",
                schema: "app",
                table: "TrotamundosPagesAboutUs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FooterHighlight",
                schema: "app",
                table: "TrotamundosPagesAboutUs",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterDescription",
                schema: "app",
                table: "TrotamundosPagesAboutUs");

            migrationBuilder.DropColumn(
                name: "FooterHighlight",
                schema: "app",
                table: "TrotamundosPagesAboutUs");
        }
    }
}

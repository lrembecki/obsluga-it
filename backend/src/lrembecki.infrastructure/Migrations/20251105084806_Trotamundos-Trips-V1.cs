using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosTripsV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "app",
                table: "TrotamundosTrip",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                schema: "app",
                table: "TrotamundosTrip",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "app",
                table: "TrotamundosTrip",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "app",
                table: "TrotamundosTrip");
        }
    }
}

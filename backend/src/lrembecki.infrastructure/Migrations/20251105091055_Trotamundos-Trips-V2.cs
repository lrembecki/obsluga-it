using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosTripsV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calendar",
                schema: "app",
                table: "TrotamundosTrip",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                schema: "app",
                table: "TrotamundosTrip",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "app",
                table: "TrotamundosTrip",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calendar",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropColumn(
                name: "EndDate",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "app",
                table: "TrotamundosTrip");
        }
    }
}

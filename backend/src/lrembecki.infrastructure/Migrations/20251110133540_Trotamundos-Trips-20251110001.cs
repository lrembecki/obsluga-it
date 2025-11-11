using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosTrips20251110001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosTripHighlightTrip",
                schema: "app");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight",
                columns: new[] { "TripId", "Order" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight",
                column: "TripId");

            migrationBuilder.CreateTable(
                name: "TrotamundosTripHighlightTrip",
                schema: "app",
                columns: table => new
                {
                    TripHighlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosTripHighlightTrip", x => new { x.TripHighlightId, x.TripId });
                    table.ForeignKey(
                        name: "FK_TrotamundosTripHighlightTrip_TrotamundosTripHighlight_TripHighlightId",
                        column: x => x.TripHighlightId,
                        principalSchema: "app",
                        principalTable: "TrotamundosTripHighlight",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosTripHighlightTrip_TrotamundosTrip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "TrotamundosTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosTripHighlightTrip_TripId",
                schema: "app",
                table: "TrotamundosTripHighlightTrip",
                column: "TripId");
        }
    }
}

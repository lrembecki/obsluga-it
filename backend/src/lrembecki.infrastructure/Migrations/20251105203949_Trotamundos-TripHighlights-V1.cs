using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosTripHighlightsV1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TrotamundosTripHighlight",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropIndex(
            name: "IX_TrotamundosTripHighlight_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.AddColumn<int>(
            name: "Order",
            schema: "app",
            table: "TrotamundosTripHighlight",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<string>(
            name: "Value",
            schema: "app",
            table: "TrotamundosTripHighlight",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

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
            name: "IX_TrotamundosTripHighlight_HighlightId",
            schema: "app",
            table: "TrotamundosTripHighlight",
            column: "HighlightId");

        migrationBuilder.CreateIndex(
            name: "IX_TrotamundosTripHighlightTrip_TripId",
            schema: "app",
            table: "TrotamundosTripHighlightTrip",
            column: "TripId");

        migrationBuilder.AddForeignKey(
            name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight",
            column: "TripId",
            principalSchema: "app",
            principalTable: "TrotamundosTrip",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropTable(
            name: "TrotamundosTripHighlightTrip",
            schema: "app");

        migrationBuilder.DropPrimaryKey(
            name: "PK_TrotamundosTripHighlight",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropIndex(
            name: "IX_TrotamundosTripHighlight_HighlightId",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropColumn(
            name: "Order",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.DropColumn(
            name: "Value",
            schema: "app",
            table: "TrotamundosTripHighlight");

        migrationBuilder.AddPrimaryKey(
            name: "PK_TrotamundosTripHighlight",
            schema: "app",
            table: "TrotamundosTripHighlight",
            columns: new[] { "HighlightId", "TripId" });

        migrationBuilder.CreateIndex(
            name: "IX_TrotamundosTripHighlight_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight",
            column: "TripId");

        migrationBuilder.AddForeignKey(
            name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
            schema: "app",
            table: "TrotamundosTripHighlight",
            column: "TripId",
            principalSchema: "app",
            principalTable: "TrotamundosTrip",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }
}

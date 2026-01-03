using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosTripAgendaV1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TrotamundosTripAgenda",
            schema: "app",
            columns: table => new
            {
                TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Order = table.Column<int>(type: "int", nullable: false),
                Content = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TrotamundosTripAgenda", x => new { x.TripId, x.Order });
                table.ForeignKey(
                    name: "FK_TrotamundosTripAgenda_TrotamundosTrip_TripId",
                    column: x => x.TripId,
                    principalSchema: "app",
                    principalTable: "TrotamundosTrip",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TrotamundosTripAgenda",
            schema: "app");
    }
}

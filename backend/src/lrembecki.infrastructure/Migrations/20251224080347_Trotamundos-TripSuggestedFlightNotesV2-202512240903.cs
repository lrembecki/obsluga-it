using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosTripSuggestedFlightNotesV2202512240903 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "SuggestedFlightNotes",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(2048)",
            maxLength: 2048,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "SuggestedFlightNotes",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(2048)",
            oldMaxLength: 2048,
            oldNullable: true);
    }
}

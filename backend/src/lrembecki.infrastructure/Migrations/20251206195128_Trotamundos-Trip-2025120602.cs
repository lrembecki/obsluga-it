using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosTrip2025120602 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Subtitle",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(2048)",
            maxLength: 2048,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "nvarchar(2048)",
            oldMaxLength: 2048);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Subtitle",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(200)",
            oldMaxLength: 200,
            oldNullable: true);

        migrationBuilder.AlterColumn<string>(
            name: "Description",
            schema: "app",
            table: "TrotamundosTrip",
            type: "nvarchar(2048)",
            maxLength: 2048,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "nvarchar(2048)",
            oldMaxLength: 2048,
            oldNullable: true);
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosTripLoyalityProgramsV1202512241622 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.CreateIndex(
            name: "IX_TrotamundosLoyalityProgram_TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram",
            column: "TripEntityId");

        migrationBuilder.AddForeignKey(
            name: "FK_TrotamundosLoyalityProgram_TrotamundosTrip_TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram",
            column: "TripEntityId",
            principalSchema: "app",
            principalTable: "TrotamundosTrip",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_TrotamundosLoyalityProgram_TrotamundosTrip_TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram");

        migrationBuilder.DropIndex(
            name: "IX_TrotamundosLoyalityProgram_TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram");

        migrationBuilder.DropColumn(
            name: "TripEntityId",
            schema: "app",
            table: "TrotamundosLoyalityProgram");
    }
}

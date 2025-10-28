using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.obsluga_it.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripSchedule_Trip_TripEntityId",
                schema: "oit",
                table: "TripSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TripSchedule_TripEntityId",
                schema: "oit",
                table: "TripSchedule");

            migrationBuilder.DropColumn(
                name: "TripEntityId",
                schema: "oit",
                table: "TripSchedule");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TripEntityId",
                schema: "oit",
                table: "TripSchedule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripSchedule_TripEntityId",
                schema: "oit",
                table: "TripSchedule",
                column: "TripEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripSchedule_Trip_TripEntityId",
                schema: "oit",
                table: "TripSchedule",
                column: "TripEntityId",
                principalSchema: "oit",
                principalTable: "Trip",
                principalColumn: "Id");
        }
    }
}

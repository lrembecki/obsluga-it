using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosTripImage2025113003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "app",
                table: "TrotamundosTripImage",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newid()");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosTripImage_TripId",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropIndex(
                name: "IX_TrotamundosTripImage_TripId",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage",
                columns: new[] { "TripId", "Order" });
        }
    }
}

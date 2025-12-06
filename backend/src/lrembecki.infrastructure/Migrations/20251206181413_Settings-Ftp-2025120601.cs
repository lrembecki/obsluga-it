using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsFtp2025120601 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropIndex(
                name: "IX_TrotamundosTripImage_ImageId",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "ImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight",
                columns: new[] { "TripId", "HighlightId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosTripImage_ImageId",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "ImageId",
                unique: true);
        }
    }
}

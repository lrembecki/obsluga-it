using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosPagesAboutUs202601120950 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosPagesAboutUsItem_Storage_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem");

            migrationBuilder.DropIndex(
                name: "IX_TrotamundosPagesAboutUsItem_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem");

            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUsItem_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosPagesAboutUsItem_Storage_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

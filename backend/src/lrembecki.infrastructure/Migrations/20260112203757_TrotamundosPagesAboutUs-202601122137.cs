using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosPagesAboutUs202601122137 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrotamundosPagesAboutUsPerson",
                schema: "app",
                columns: table => new
                {
                    AboutUsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AboutUsEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesAboutUsPerson", x => new { x.AboutUsId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUsPerson_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUsPerson_TrotamundosPagesAboutUs_AboutUsEntityId",
                        column: x => x.AboutUsEntityId,
                        principalSchema: "app",
                        principalTable: "TrotamundosPagesAboutUs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUsPerson_AboutUsEntityId",
                schema: "app",
                table: "TrotamundosPagesAboutUsPerson",
                column: "AboutUsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUsPerson_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsPerson",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosPagesAboutUsPerson",
                schema: "app");
        }
    }
}

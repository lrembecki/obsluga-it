using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosPagesAboutUs202601111054 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrotamundosPagesAboutUs",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesAboutUs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUs_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUs_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrotamundosPagesAboutUsItem",
                schema: "app",
                columns: table => new
                {
                    AboutUsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesAboutUsItem", x => new { x.AboutUsId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUsItem_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesAboutUsItem_TrotamundosPagesAboutUs_AboutUsId",
                        column: x => x.AboutUsId,
                        principalSchema: "app",
                        principalTable: "TrotamundosPagesAboutUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUs_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUs",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUs_SubscriptionId",
                schema: "app",
                table: "TrotamundosPagesAboutUs",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesAboutUsItem_ImageId",
                schema: "app",
                table: "TrotamundosPagesAboutUsItem",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosPagesAboutUsItem",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TrotamundosPagesAboutUs",
                schema: "app");
        }
    }
}

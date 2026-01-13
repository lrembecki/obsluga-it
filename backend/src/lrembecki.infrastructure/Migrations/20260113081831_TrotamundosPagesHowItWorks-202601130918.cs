using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosPagesHowItWorks202601130918 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrotamundosPagesHowItWorks",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HeaderText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FooterText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesHowItWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHowItWorks_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrotamundosPagesHowItWorksItem",
                schema: "app",
                columns: table => new
                {
                    HowItWorksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesHowItWorksItem", x => new { x.HowItWorksId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHowItWorksItem_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHowItWorksItem_TrotamundosPagesHowItWorks_HowItWorksId",
                        column: x => x.HowItWorksId,
                        principalSchema: "app",
                        principalTable: "TrotamundosPagesHowItWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHowItWorks_SubscriptionId",
                schema: "app",
                table: "TrotamundosPagesHowItWorks",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHowItWorksItem_ImageId",
                schema: "app",
                table: "TrotamundosPagesHowItWorksItem",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosPagesHowItWorksItem",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TrotamundosPagesHowItWorks",
                schema: "app");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosPagesHome202601192340 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "app",
                table: "TrotamundosPagesHowItWorksItem",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "HeaderText",
                schema: "app",
                table: "TrotamundosPagesHowItWorks",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FooterText",
                schema: "app",
                table: "TrotamundosPagesHowItWorks",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "TrotamundosPagesHome",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BackgroundVideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrailerVideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesHome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHome_Storage_BackgroundVideoId",
                        column: x => x.BackgroundVideoId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHome_Storage_TrailerVideoId",
                        column: x => x.TrailerVideoId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHome_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrotamundosPagesHomeOpinion",
                schema: "app",
                columns: table => new
                {
                    HomeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosPagesHomeOpinion", x => new { x.HomeId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHomeOpinion_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosPagesHomeOpinion_TrotamundosPagesHome_HomeId",
                        column: x => x.HomeId,
                        principalSchema: "app",
                        principalTable: "TrotamundosPagesHome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHome_BackgroundVideoId",
                schema: "app",
                table: "TrotamundosPagesHome",
                column: "BackgroundVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHome_SubscriptionId",
                schema: "app",
                table: "TrotamundosPagesHome",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHome_TrailerVideoId",
                schema: "app",
                table: "TrotamundosPagesHome",
                column: "TrailerVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosPagesHomeOpinion_ImageId",
                schema: "app",
                table: "TrotamundosPagesHomeOpinion",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosPagesHomeOpinion",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TrotamundosPagesHome",
                schema: "app");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "app",
                table: "TrotamundosPagesHowItWorksItem",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "HeaderText",
                schema: "app",
                table: "TrotamundosPagesHowItWorks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AlterColumn<string>(
                name: "FooterText",
                schema: "app",
                table: "TrotamundosPagesHowItWorks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);
        }
    }
}

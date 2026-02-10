using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundoIndividualTrip202602101134 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrotamundosIndividualTrip",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosIndividualTrip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrotamundosIndividualTrip_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrotamundosIndividualTripItem",
                schema: "app",
                columns: table => new
                {
                    IndividualTripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Uom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosIndividualTripItem", x => new { x.IndividualTripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosIndividualTripItem_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrotamundosIndividualTripItem_TrotamundosIndividualTrip_IndividualTripId",
                        column: x => x.IndividualTripId,
                        principalSchema: "app",
                        principalTable: "TrotamundosIndividualTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrotamundosIndividualTripStepItem",
                schema: "app",
                columns: table => new
                {
                    IndividualTripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrotamundosIndividualTripStepItem", x => new { x.IndividualTripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TrotamundosIndividualTripStepItem_TrotamundosIndividualTrip_IndividualTripId",
                        column: x => x.IndividualTripId,
                        principalSchema: "app",
                        principalTable: "TrotamundosIndividualTrip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosIndividualTrip_SubscriptionId",
                schema: "app",
                table: "TrotamundosIndividualTrip",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrotamundosIndividualTripItem_ImageId",
                schema: "app",
                table: "TrotamundosIndividualTripItem",
                column: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrotamundosIndividualTripItem",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TrotamundosIndividualTripStepItem",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TrotamundosIndividualTrip",
                schema: "app");
        }
    }
}

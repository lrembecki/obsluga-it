using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.obsluga_it.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionUser_Subscription_SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionUser_User_UserId",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionUser_SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.DropColumn(
                name: "SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.AddColumn<Guid>(
                name: "TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripSuggestedFlight_TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight",
                column: "TripEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionUser_User_UserId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "UserId",
                principalSchema: "oit",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSuggestedFlight_Trip_TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight",
                column: "TripEntityId",
                principalSchema: "oit",
                principalTable: "Trip",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionUser_User_UserId",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSuggestedFlight_Trip_TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight");

            migrationBuilder.DropIndex(
                name: "IX_TripSuggestedFlight_TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight");

            migrationBuilder.DropColumn(
                name: "TripEntityId",
                schema: "oit",
                table: "TripSuggestedFlight");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "SubscriptionEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionUser_Subscription_SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "SubscriptionEntityId",
                principalSchema: "oit",
                principalTable: "Subscription",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionUser_User_UserId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "UserId",
                principalSchema: "oit",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

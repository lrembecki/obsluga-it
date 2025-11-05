using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TrotamundosStructureFixV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advantage_Subscription_SubscriptionId",
                schema: "app",
                table: "Advantage");

            migrationBuilder.DropForeignKey(
                name: "FK_Highlight_Subscription_SubscriptionId",
                schema: "app",
                table: "Highlight");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyalityProgram_Storage_ImageId",
                schema: "app",
                table: "LoyalityProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_LoyalityProgram_Subscription_SubscriptionId",
                schema: "app",
                table: "LoyalityProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_Trip_Subscription_SubscriptionId",
                schema: "app",
                table: "Trip");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAdvantage_Advantage_AdvantageId",
                schema: "app",
                table: "TripAdvantage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripAdvantage_Trip_TripId",
                schema: "app",
                table: "TripAdvantage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripHighlight_Highlight_HighlightId",
                schema: "app",
                table: "TripHighlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TripHighlight_Trip_TripId",
                schema: "app",
                table: "TripHighlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TripImage_Storage_ImageId",
                schema: "app",
                table: "TripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripImage_Trip_TripId",
                schema: "app",
                table: "TripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPaymentSchedule_Trip_TripId",
                schema: "app",
                table: "TripPaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPriceInclude_Trip_TripId",
                schema: "app",
                table: "TripPriceInclude");

            migrationBuilder.DropForeignKey(
                name: "FK_TripRequirement_Trip_TripId",
                schema: "app",
                table: "TripRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSchedule_Trip_TripId",
                schema: "app",
                table: "TripSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSuggestedFlight_Storage_ImageId",
                schema: "app",
                table: "TripSuggestedFlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TripSuggestedFlight_Trip_TripId",
                schema: "app",
                table: "TripSuggestedFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripSuggestedFlight",
                schema: "app",
                table: "TripSuggestedFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripSchedule",
                schema: "app",
                table: "TripSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripRequirement",
                schema: "app",
                table: "TripRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripPriceInclude",
                schema: "app",
                table: "TripPriceInclude");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripPaymentSchedule",
                schema: "app",
                table: "TripPaymentSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripImage",
                schema: "app",
                table: "TripImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripHighlight",
                schema: "app",
                table: "TripHighlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripAdvantage",
                schema: "app",
                table: "TripAdvantage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                schema: "app",
                table: "Trip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoyalityProgram",
                schema: "app",
                table: "LoyalityProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Highlight",
                schema: "app",
                table: "Highlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advantage",
                schema: "app",
                table: "Advantage");

            migrationBuilder.RenameTable(
                name: "TripSuggestedFlight",
                schema: "app",
                newName: "TrotamundosTripSuggestedFlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripSchedule",
                schema: "app",
                newName: "TrotamundosTripSchedule",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripRequirement",
                schema: "app",
                newName: "TrotamundosTripRequirement",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripPriceInclude",
                schema: "app",
                newName: "TrotamundosTripPriceInclude",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripPaymentSchedule",
                schema: "app",
                newName: "TrotamundosTripPaymentSchedule",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripImage",
                schema: "app",
                newName: "TrotamundosTripImage",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripHighlight",
                schema: "app",
                newName: "TrotamundosTripHighlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TripAdvantage",
                schema: "app",
                newName: "TrotamundosTripAdvantage",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "Trip",
                schema: "app",
                newName: "TrotamundosTrip",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "LoyalityProgram",
                schema: "app",
                newName: "TrotamundosLoyalityProgram",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "Highlight",
                schema: "app",
                newName: "TrotamundosHighlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "Advantage",
                schema: "app",
                newName: "TrotamundosAdvantage",
                newSchema: "app");

            migrationBuilder.RenameIndex(
                name: "IX_TripSuggestedFlight_ImageId",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight",
                newName: "IX_TrotamundosTripSuggestedFlight_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_TripImage_ImageId",
                schema: "app",
                table: "TrotamundosTripImage",
                newName: "IX_TrotamundosTripImage_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_TripHighlight_TripId",
                schema: "app",
                table: "TrotamundosTripHighlight",
                newName: "IX_TrotamundosTripHighlight_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripAdvantage_TripId",
                schema: "app",
                table: "TrotamundosTripAdvantage",
                newName: "IX_TrotamundosTripAdvantage_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Trip_SubscriptionId",
                schema: "app",
                table: "TrotamundosTrip",
                newName: "IX_TrotamundosTrip_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyalityProgram_SubscriptionId",
                schema: "app",
                table: "TrotamundosLoyalityProgram",
                newName: "IX_TrotamundosLoyalityProgram_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_LoyalityProgram_ImageId",
                schema: "app",
                table: "TrotamundosLoyalityProgram",
                newName: "IX_TrotamundosLoyalityProgram_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Highlight_SubscriptionId",
                schema: "app",
                table: "TrotamundosHighlight",
                newName: "IX_TrotamundosHighlight_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_Advantage_SubscriptionId",
                schema: "app",
                table: "TrotamundosAdvantage",
                newName: "IX_TrotamundosAdvantage_SubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripSuggestedFlight",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripSchedule",
                schema: "app",
                table: "TrotamundosTripSchedule",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripRequirement",
                schema: "app",
                table: "TrotamundosTripRequirement",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripPriceInclude",
                schema: "app",
                table: "TrotamundosTripPriceInclude",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripPaymentSchedule",
                schema: "app",
                table: "TrotamundosTripPaymentSchedule",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight",
                columns: new[] { "HighlightId", "TripId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTripAdvantage",
                schema: "app",
                table: "TrotamundosTripAdvantage",
                columns: new[] { "AdvantageId", "TripId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosTrip",
                schema: "app",
                table: "TrotamundosTrip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosLoyalityProgram",
                schema: "app",
                table: "TrotamundosLoyalityProgram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosHighlight",
                schema: "app",
                table: "TrotamundosHighlight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrotamundosAdvantage",
                schema: "app",
                table: "TrotamundosAdvantage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosAdvantage_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosAdvantage",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosHighlight_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosHighlight",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosLoyalityProgram_Storage_ImageId",
                schema: "app",
                table: "TrotamundosLoyalityProgram",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosLoyalityProgram_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosLoyalityProgram",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTrip_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosTrip",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripAdvantage_TrotamundosAdvantage_AdvantageId",
                schema: "app",
                table: "TrotamundosTripAdvantage",
                column: "AdvantageId",
                principalSchema: "app",
                principalTable: "TrotamundosAdvantage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripAdvantage_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripAdvantage",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripHighlight_TrotamundosHighlight_HighlightId",
                schema: "app",
                table: "TrotamundosTripHighlight",
                column: "HighlightId",
                principalSchema: "app",
                principalTable: "TrotamundosHighlight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripHighlight",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripImage_Storage_ImageId",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripImage_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripImage",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripPaymentSchedule_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripPaymentSchedule",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripPriceInclude_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripPriceInclude",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripRequirement_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripRequirement",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripSchedule_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripSchedule",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripSuggestedFlight_Storage_ImageId",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrotamundosTripSuggestedFlight_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight",
                column: "TripId",
                principalSchema: "app",
                principalTable: "TrotamundosTrip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosAdvantage_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosAdvantage");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosHighlight_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosHighlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosLoyalityProgram_Storage_ImageId",
                schema: "app",
                table: "TrotamundosLoyalityProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosLoyalityProgram_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosLoyalityProgram");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTrip_Subscription_SubscriptionId",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripAdvantage_TrotamundosAdvantage_AdvantageId",
                schema: "app",
                table: "TrotamundosTripAdvantage");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripAdvantage_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripAdvantage");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripHighlight_TrotamundosHighlight_HighlightId",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripHighlight_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripImage_Storage_ImageId",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripImage_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripPaymentSchedule_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripPaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripPriceInclude_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripPriceInclude");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripRequirement_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripSchedule_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripSuggestedFlight_Storage_ImageId",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight");

            migrationBuilder.DropForeignKey(
                name: "FK_TrotamundosTripSuggestedFlight_TrotamundosTrip_TripId",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripSuggestedFlight",
                schema: "app",
                table: "TrotamundosTripSuggestedFlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripSchedule",
                schema: "app",
                table: "TrotamundosTripSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripRequirement",
                schema: "app",
                table: "TrotamundosTripRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripPriceInclude",
                schema: "app",
                table: "TrotamundosTripPriceInclude");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripPaymentSchedule",
                schema: "app",
                table: "TrotamundosTripPaymentSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripImage",
                schema: "app",
                table: "TrotamundosTripImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripHighlight",
                schema: "app",
                table: "TrotamundosTripHighlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTripAdvantage",
                schema: "app",
                table: "TrotamundosTripAdvantage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosTrip",
                schema: "app",
                table: "TrotamundosTrip");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosLoyalityProgram",
                schema: "app",
                table: "TrotamundosLoyalityProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosHighlight",
                schema: "app",
                table: "TrotamundosHighlight");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrotamundosAdvantage",
                schema: "app",
                table: "TrotamundosAdvantage");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripSuggestedFlight",
                schema: "app",
                newName: "TripSuggestedFlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripSchedule",
                schema: "app",
                newName: "TripSchedule",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripRequirement",
                schema: "app",
                newName: "TripRequirement",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripPriceInclude",
                schema: "app",
                newName: "TripPriceInclude",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripPaymentSchedule",
                schema: "app",
                newName: "TripPaymentSchedule",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripImage",
                schema: "app",
                newName: "TripImage",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripHighlight",
                schema: "app",
                newName: "TripHighlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTripAdvantage",
                schema: "app",
                newName: "TripAdvantage",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosTrip",
                schema: "app",
                newName: "Trip",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosLoyalityProgram",
                schema: "app",
                newName: "LoyalityProgram",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosHighlight",
                schema: "app",
                newName: "Highlight",
                newSchema: "app");

            migrationBuilder.RenameTable(
                name: "TrotamundosAdvantage",
                schema: "app",
                newName: "Advantage",
                newSchema: "app");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosTripSuggestedFlight_ImageId",
                schema: "app",
                table: "TripSuggestedFlight",
                newName: "IX_TripSuggestedFlight_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosTripImage_ImageId",
                schema: "app",
                table: "TripImage",
                newName: "IX_TripImage_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosTripHighlight_TripId",
                schema: "app",
                table: "TripHighlight",
                newName: "IX_TripHighlight_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosTripAdvantage_TripId",
                schema: "app",
                table: "TripAdvantage",
                newName: "IX_TripAdvantage_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosTrip_SubscriptionId",
                schema: "app",
                table: "Trip",
                newName: "IX_Trip_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosLoyalityProgram_SubscriptionId",
                schema: "app",
                table: "LoyalityProgram",
                newName: "IX_LoyalityProgram_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosLoyalityProgram_ImageId",
                schema: "app",
                table: "LoyalityProgram",
                newName: "IX_LoyalityProgram_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosHighlight_SubscriptionId",
                schema: "app",
                table: "Highlight",
                newName: "IX_Highlight_SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_TrotamundosAdvantage_SubscriptionId",
                schema: "app",
                table: "Advantage",
                newName: "IX_Advantage_SubscriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripSuggestedFlight",
                schema: "app",
                table: "TripSuggestedFlight",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripSchedule",
                schema: "app",
                table: "TripSchedule",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripRequirement",
                schema: "app",
                table: "TripRequirement",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripPriceInclude",
                schema: "app",
                table: "TripPriceInclude",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripPaymentSchedule",
                schema: "app",
                table: "TripPaymentSchedule",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripImage",
                schema: "app",
                table: "TripImage",
                columns: new[] { "TripId", "Order" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripHighlight",
                schema: "app",
                table: "TripHighlight",
                columns: new[] { "HighlightId", "TripId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripAdvantage",
                schema: "app",
                table: "TripAdvantage",
                columns: new[] { "AdvantageId", "TripId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                schema: "app",
                table: "Trip",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoyalityProgram",
                schema: "app",
                table: "LoyalityProgram",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Highlight",
                schema: "app",
                table: "Highlight",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advantage",
                schema: "app",
                table: "Advantage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Advantage_Subscription_SubscriptionId",
                schema: "app",
                table: "Advantage",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Highlight_Subscription_SubscriptionId",
                schema: "app",
                table: "Highlight",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyalityProgram_Storage_ImageId",
                schema: "app",
                table: "LoyalityProgram",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoyalityProgram_Subscription_SubscriptionId",
                schema: "app",
                table: "LoyalityProgram",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trip_Subscription_SubscriptionId",
                schema: "app",
                table: "Trip",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAdvantage_Advantage_AdvantageId",
                schema: "app",
                table: "TripAdvantage",
                column: "AdvantageId",
                principalSchema: "app",
                principalTable: "Advantage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripAdvantage_Trip_TripId",
                schema: "app",
                table: "TripAdvantage",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripHighlight_Highlight_HighlightId",
                schema: "app",
                table: "TripHighlight",
                column: "HighlightId",
                principalSchema: "app",
                principalTable: "Highlight",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripHighlight_Trip_TripId",
                schema: "app",
                table: "TripHighlight",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripImage_Storage_ImageId",
                schema: "app",
                table: "TripImage",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripImage_Trip_TripId",
                schema: "app",
                table: "TripImage",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPaymentSchedule_Trip_TripId",
                schema: "app",
                table: "TripPaymentSchedule",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPriceInclude_Trip_TripId",
                schema: "app",
                table: "TripPriceInclude",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripRequirement_Trip_TripId",
                schema: "app",
                table: "TripRequirement",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSchedule_Trip_TripId",
                schema: "app",
                table: "TripSchedule",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSuggestedFlight_Storage_ImageId",
                schema: "app",
                table: "TripSuggestedFlight",
                column: "ImageId",
                principalSchema: "app",
                principalTable: "Storage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TripSuggestedFlight_Trip_TripId",
                schema: "app",
                table: "TripSuggestedFlight",
                column: "TripId",
                principalSchema: "app",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

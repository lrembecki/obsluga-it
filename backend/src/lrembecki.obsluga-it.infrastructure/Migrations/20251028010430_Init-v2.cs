using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.obsluga_it.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Actor_ActorEntityId",
                schema: "oit",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_TripImage_Trip_TripEntityId",
                schema: "oit",
                table: "TripImage");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPaymentSchedule_Trip_TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPriceInclude_Trip_TripEntityId",
                schema: "oit",
                table: "TripPriceInclude");

            migrationBuilder.DropIndex(
                name: "IX_TripPriceInclude_TripEntityId",
                schema: "oit",
                table: "TripPriceInclude");

            migrationBuilder.DropIndex(
                name: "IX_TripPaymentSchedule_TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule");

            migrationBuilder.DropIndex(
                name: "IX_TripImage_TripEntityId",
                schema: "oit",
                table: "TripImage");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ActorEntityId",
                schema: "oit",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TripEntityId",
                schema: "oit",
                table: "TripPriceInclude");

            migrationBuilder.DropColumn(
                name: "TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule");

            migrationBuilder.DropColumn(
                name: "TripEntityId",
                schema: "oit",
                table: "TripImage");

            migrationBuilder.DropColumn(
                name: "ActorEntityId",
                schema: "oit",
                table: "Contact");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "oit",
                table: "SubscriptionUser",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                schema: "oit",
                table: "SubscriptionUser");

            migrationBuilder.AddColumn<Guid>(
                name: "TripEntityId",
                schema: "oit",
                table: "TripPriceInclude",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TripEntityId",
                schema: "oit",
                table: "TripImage",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ActorEntityId",
                schema: "oit",
                table: "Contact",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripPriceInclude_TripEntityId",
                schema: "oit",
                table: "TripPriceInclude",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPaymentSchedule_TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripImage_TripEntityId",
                schema: "oit",
                table: "TripImage",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ActorEntityId",
                schema: "oit",
                table: "Contact",
                column: "ActorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Actor_ActorEntityId",
                schema: "oit",
                table: "Contact",
                column: "ActorEntityId",
                principalSchema: "oit",
                principalTable: "Actor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripImage_Trip_TripEntityId",
                schema: "oit",
                table: "TripImage",
                column: "TripEntityId",
                principalSchema: "oit",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripPaymentSchedule_Trip_TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule",
                column: "TripEntityId",
                principalSchema: "oit",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripPriceInclude_Trip_TripEntityId",
                schema: "oit",
                table: "TripPriceInclude",
                column: "TripEntityId",
                principalSchema: "oit",
                principalTable: "Trip",
                principalColumn: "Id");
        }
    }
}

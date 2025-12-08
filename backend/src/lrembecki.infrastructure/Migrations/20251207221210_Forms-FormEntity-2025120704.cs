using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FormsFormEntity2025120704 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormEntity_Subscription_SubscriptionId",
                schema: "app",
                table: "FormEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_FormEntity_FormId",
                schema: "app",
                table: "FormFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormEntity",
                schema: "app",
                table: "FormEntity");

            migrationBuilder.RenameTable(
                name: "FormEntity",
                schema: "app",
                newName: "Forms",
                newSchema: "app");

            migrationBuilder.RenameIndex(
                name: "IX_FormEntity_SubscriptionId",
                schema: "app",
                table: "Forms",
                newName: "IX_Forms_SubscriptionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "app",
                table: "Forms",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forms",
                schema: "app",
                table: "Forms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_Name",
                schema: "app",
                table: "Forms",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_Forms_FormId",
                schema: "app",
                table: "FormFields",
                column: "FormId",
                principalSchema: "app",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Subscription_SubscriptionId",
                schema: "app",
                table: "Forms",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormFields_Forms_FormId",
                schema: "app",
                table: "FormFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Subscription_SubscriptionId",
                schema: "app",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forms",
                schema: "app",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_Name",
                schema: "app",
                table: "Forms");

            migrationBuilder.RenameTable(
                name: "Forms",
                schema: "app",
                newName: "FormEntity",
                newSchema: "app");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_SubscriptionId",
                schema: "app",
                table: "FormEntity",
                newName: "IX_FormEntity_SubscriptionId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "app",
                table: "FormEntity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormEntity",
                schema: "app",
                table: "FormEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormEntity_Subscription_SubscriptionId",
                schema: "app",
                table: "FormEntity",
                column: "SubscriptionId",
                principalSchema: "app",
                principalTable: "Subscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormFields_FormEntity_FormId",
                schema: "app",
                table: "FormFields",
                column: "FormId",
                principalSchema: "app",
                principalTable: "FormEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

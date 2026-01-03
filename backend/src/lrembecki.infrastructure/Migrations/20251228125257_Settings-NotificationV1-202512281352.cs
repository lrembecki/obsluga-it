using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class SettingsNotificationV1202512281352 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ContactEntity_Subscription_SubscriptionId",
            schema: "app",
            table: "ContactEntity");

        migrationBuilder.DropForeignKey(
            name: "FK_EmailEntity_Subscription_SubscriptionId",
            schema: "app",
            table: "EmailEntity");

        migrationBuilder.DropPrimaryKey(
            name: "PK_EmailEntity",
            schema: "app",
            table: "EmailEntity");

        migrationBuilder.DropPrimaryKey(
            name: "PK_ContactEntity",
            schema: "app",
            table: "ContactEntity");

        migrationBuilder.RenameTable(
            name: "EmailEntity",
            schema: "app",
            newName: "Email",
            newSchema: "app");

        migrationBuilder.RenameTable(
            name: "ContactEntity",
            schema: "app",
            newName: "Contact",
            newSchema: "app");

        migrationBuilder.RenameIndex(
            name: "IX_EmailEntity_SubscriptionId",
            schema: "app",
            table: "Email",
            newName: "IX_Email_SubscriptionId");

        migrationBuilder.RenameIndex(
            name: "IX_ContactEntity_SubscriptionId",
            schema: "app",
            table: "Contact",
            newName: "IX_Contact_SubscriptionId");

        migrationBuilder.AddColumn<Guid>(
            name: "NotificationId",
            schema: "app",
            table: "FormDefinition",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddPrimaryKey(
            name: "PK_Email",
            schema: "app",
            table: "Email",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Contact",
            schema: "app",
            table: "Contact",
            column: "Id");

        migrationBuilder.CreateTable(
            name: "EmailTemplate",
            schema: "app",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                TemplateHtmlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                table.ForeignKey(
                    name: "FK_EmailTemplate_Storage_TemplateHtmlId",
                    column: x => x.TemplateHtmlId,
                    principalSchema: "app",
                    principalTable: "Storage",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_EmailTemplate_Subscription_SubscriptionId",
                    column: x => x.SubscriptionId,
                    principalSchema: "app",
                    principalTable: "Subscription",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Notification",
            schema: "app",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notification", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notification_Subscription_SubscriptionId",
                    column: x => x.SubscriptionId,
                    principalSchema: "app",
                    principalTable: "Subscription",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "EmailTemplateField",
            schema: "app",
            columns: table => new
            {
                EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_EmailTemplateField", x => new { x.EmailTemplateId, x.FieldName });
                table.ForeignKey(
                    name: "FK_EmailTemplateField_EmailTemplate_EmailTemplateId",
                    column: x => x.EmailTemplateId,
                    principalSchema: "app",
                    principalTable: "EmailTemplate",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "NotificationEmail",
            schema: "app",
            columns: table => new
            {
                NotificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EmailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NotificationEmail", x => x.NotificationId);
                table.ForeignKey(
                    name: "FK_NotificationEmail_EmailTemplate_EmailTemplateId",
                    column: x => x.EmailTemplateId,
                    principalSchema: "app",
                    principalTable: "EmailTemplate",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_NotificationEmail_Email_EmailId",
                    column: x => x.EmailId,
                    principalSchema: "app",
                    principalTable: "Email",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_NotificationEmail_Notification_NotificationId",
                    column: x => x.NotificationId,
                    principalSchema: "app",
                    principalTable: "Notification",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FormDefinition_NotificationId",
            schema: "app",
            table: "FormDefinition",
            column: "NotificationId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailTemplate_SubscriptionId",
            schema: "app",
            table: "EmailTemplate",
            column: "SubscriptionId");

        migrationBuilder.CreateIndex(
            name: "IX_EmailTemplate_TemplateHtmlId",
            schema: "app",
            table: "EmailTemplate",
            column: "TemplateHtmlId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Notification_SubscriptionId",
            schema: "app",
            table: "Notification",
            column: "SubscriptionId");

        migrationBuilder.CreateIndex(
            name: "IX_NotificationEmail_EmailId",
            schema: "app",
            table: "NotificationEmail",
            column: "EmailId");

        migrationBuilder.CreateIndex(
            name: "IX_NotificationEmail_EmailTemplateId",
            schema: "app",
            table: "NotificationEmail",
            column: "EmailTemplateId");

        migrationBuilder.AddForeignKey(
            name: "FK_Contact_Subscription_SubscriptionId",
            schema: "app",
            table: "Contact",
            column: "SubscriptionId",
            principalSchema: "app",
            principalTable: "Subscription",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_Email_Subscription_SubscriptionId",
            schema: "app",
            table: "Email",
            column: "SubscriptionId",
            principalSchema: "app",
            principalTable: "Subscription",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_FormDefinition_Notification_NotificationId",
            schema: "app",
            table: "FormDefinition",
            column: "NotificationId",
            principalSchema: "app",
            principalTable: "Notification",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Contact_Subscription_SubscriptionId",
            schema: "app",
            table: "Contact");

        migrationBuilder.DropForeignKey(
            name: "FK_Email_Subscription_SubscriptionId",
            schema: "app",
            table: "Email");

        migrationBuilder.DropForeignKey(
            name: "FK_FormDefinition_Notification_NotificationId",
            schema: "app",
            table: "FormDefinition");

        migrationBuilder.DropTable(
            name: "EmailTemplateField",
            schema: "app");

        migrationBuilder.DropTable(
            name: "NotificationEmail",
            schema: "app");

        migrationBuilder.DropTable(
            name: "EmailTemplate",
            schema: "app");

        migrationBuilder.DropTable(
            name: "Notification",
            schema: "app");

        migrationBuilder.DropIndex(
            name: "IX_FormDefinition_NotificationId",
            schema: "app",
            table: "FormDefinition");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Email",
            schema: "app",
            table: "Email");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Contact",
            schema: "app",
            table: "Contact");

        migrationBuilder.DropColumn(
            name: "NotificationId",
            schema: "app",
            table: "FormDefinition");

        migrationBuilder.RenameTable(
            name: "Email",
            schema: "app",
            newName: "EmailEntity",
            newSchema: "app");

        migrationBuilder.RenameTable(
            name: "Contact",
            schema: "app",
            newName: "ContactEntity",
            newSchema: "app");

        migrationBuilder.RenameIndex(
            name: "IX_Email_SubscriptionId",
            schema: "app",
            table: "EmailEntity",
            newName: "IX_EmailEntity_SubscriptionId");

        migrationBuilder.RenameIndex(
            name: "IX_Contact_SubscriptionId",
            schema: "app",
            table: "ContactEntity",
            newName: "IX_ContactEntity_SubscriptionId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_EmailEntity",
            schema: "app",
            table: "EmailEntity",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ContactEntity",
            schema: "app",
            table: "ContactEntity",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_ContactEntity_Subscription_SubscriptionId",
            schema: "app",
            table: "ContactEntity",
            column: "SubscriptionId",
            principalSchema: "app",
            principalTable: "Subscription",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_EmailEntity_Subscription_SubscriptionId",
            schema: "app",
            table: "EmailEntity",
            column: "SubscriptionId",
            principalSchema: "app",
            principalTable: "Subscription",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}

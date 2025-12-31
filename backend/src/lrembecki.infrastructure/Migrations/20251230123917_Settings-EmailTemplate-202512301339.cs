using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsEmailTemplate202512301339 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                schema: "app",
                table: "EmailTemplate",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmailTemplate_Contacts_Bcc",
                schema: "app",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate_Contacts_Bcc", x => new { x.ContactId, x.EmailTemplateId });
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_Bcc_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "app",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_Bcc_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalSchema: "app",
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate_Contacts_Cc",
                schema: "app",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate_Contacts_Cc", x => new { x.ContactId, x.EmailTemplateId });
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_Cc_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "app",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_Cc_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalSchema: "app",
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate_Contacts_To",
                schema: "app",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate_Contacts_To", x => new { x.ContactId, x.EmailTemplateId });
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_To_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "app",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_Contacts_To_EmailTemplate_EmailTemplateId",
                        column: x => x.EmailTemplateId,
                        principalSchema: "app",
                        principalTable: "EmailTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_Contacts_Bcc_EmailTemplateId",
                schema: "app",
                table: "EmailTemplate_Contacts_Bcc",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_Contacts_Cc_EmailTemplateId",
                schema: "app",
                table: "EmailTemplate_Contacts_Cc",
                column: "EmailTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_Contacts_To_EmailTemplateId",
                schema: "app",
                table: "EmailTemplate_Contacts_To",
                column: "EmailTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailTemplate_Contacts_Bcc",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmailTemplate_Contacts_Cc",
                schema: "app");

            migrationBuilder.DropTable(
                name: "EmailTemplate_Contacts_To",
                schema: "app");

            migrationBuilder.DropColumn(
                name: "Subject",
                schema: "app",
                table: "EmailTemplate");
        }
    }
}

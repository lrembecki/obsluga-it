using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SettingsCompanyContact202601031754 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyEmailContact",
                schema: "app",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyEmailContact", x => new { x.CompanyId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_CompanyEmailContact_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "app",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyEmailContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "app",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPhoneContact",
                schema: "app",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPhoneContact", x => new { x.CompanyId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_CompanyPhoneContact_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "app",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyPhoneContact_Contact_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "app",
                        principalTable: "Contact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyEmailContact_ContactId",
                schema: "app",
                table: "CompanyEmailContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPhoneContact_ContactId",
                schema: "app",
                table: "CompanyPhoneContact",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyEmailContact",
                schema: "app");

            migrationBuilder.DropTable(
                name: "CompanyPhoneContact",
                schema: "app");
        }
    }
}

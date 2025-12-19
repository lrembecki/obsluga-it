using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FormsFixes202512191735 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormField",
                schema: "app",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "FormDefinitionFieldId",
                schema: "app",
                table: "FormField");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "app",
                table: "FormField",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormField",
                schema: "app",
                table: "FormField",
                columns: new[] { "FormId", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FormField",
                schema: "app",
                table: "FormField");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "app",
                table: "FormField");

            migrationBuilder.AddColumn<Guid>(
                name: "FormDefinitionFieldId",
                schema: "app",
                table: "FormField",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormField",
                schema: "app",
                table: "FormField",
                columns: new[] { "FormId", "FormDefinitionFieldId" });
        }
    }
}

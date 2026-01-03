using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class SettingsNotificationV3202512281540 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FormDefinitionEmailNotificationMapping_FormDefinition_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping");

        migrationBuilder.DropForeignKey(
            name: "FK_FormDefinitionField_FormDefinition_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField");

        migrationBuilder.DropIndex(
            name: "IX_FormDefinitionField_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField");

        migrationBuilder.DropIndex(
            name: "IX_FormDefinitionEmailNotificationMapping_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping");

        migrationBuilder.DropColumn(
            name: "FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField");

        migrationBuilder.DropColumn(
            name: "FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping");

        migrationBuilder.AddForeignKey(
            name: "FK_FormDefinitionEmailNotificationMapping_FormDefinition_FormDefinitionId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping",
            column: "FormDefinitionId",
            principalSchema: "app",
            principalTable: "FormDefinition",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_FormDefinitionField_FormDefinition_FormDefinitionId",
            schema: "app",
            table: "FormDefinitionField",
            column: "FormDefinitionId",
            principalSchema: "app",
            principalTable: "FormDefinition",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FormDefinitionEmailNotificationMapping_FormDefinition_FormDefinitionId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping");

        migrationBuilder.DropForeignKey(
            name: "FK_FormDefinitionField_FormDefinition_FormDefinitionId",
            schema: "app",
            table: "FormDefinitionField");

        migrationBuilder.AddColumn<Guid>(
            name: "FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.CreateIndex(
            name: "IX_FormDefinitionField_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField",
            column: "FormDefinitionEntityId");

        migrationBuilder.CreateIndex(
            name: "IX_FormDefinitionEmailNotificationMapping_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping",
            column: "FormDefinitionEntityId");

        migrationBuilder.AddForeignKey(
            name: "FK_FormDefinitionEmailNotificationMapping_FormDefinition_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping",
            column: "FormDefinitionEntityId",
            principalSchema: "app",
            principalTable: "FormDefinition",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_FormDefinitionField_FormDefinition_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionField",
            column: "FormDefinitionEntityId",
            principalSchema: "app",
            principalTable: "FormDefinition",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}

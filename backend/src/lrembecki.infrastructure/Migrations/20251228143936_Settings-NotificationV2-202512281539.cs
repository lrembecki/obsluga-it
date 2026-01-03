using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class SettingsNotificationV2202512281539 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "FormDefinitionEmailNotificationMapping",
            schema: "app",
            columns: table => new
            {
                FormDefinitionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FormDefinitionFieldName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                EmailTemplateFieldName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                FormDefinitionEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FormDefinitionEmailNotificationMapping", x => new { x.FormDefinitionId, x.FormDefinitionFieldName, x.EmailTemplateFieldName });
                table.ForeignKey(
                    name: "FK_FormDefinitionEmailNotificationMapping_FormDefinition_FormDefinitionEntityId",
                    column: x => x.FormDefinitionEntityId,
                    principalSchema: "app",
                    principalTable: "FormDefinition",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FormDefinitionEmailNotificationMapping_FormDefinitionEntityId",
            schema: "app",
            table: "FormDefinitionEmailNotificationMapping",
            column: "FormDefinitionEntityId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FormDefinitionEmailNotificationMapping",
            schema: "app");
    }
}

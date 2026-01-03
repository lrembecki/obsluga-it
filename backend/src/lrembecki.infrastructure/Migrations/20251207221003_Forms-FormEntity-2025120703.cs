using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class FormsFormEntity2025120703 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "FormEntity",
            schema: "app",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FormEntity", x => x.Id);
                table.ForeignKey(
                    name: "FK_FormEntity_Subscription_SubscriptionId",
                    column: x => x.SubscriptionId,
                    principalSchema: "app",
                    principalTable: "Subscription",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "FormFields",
            schema: "app",
            columns: table => new
            {
                FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                Value = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FormFields", x => new { x.FormId, x.Name });
                table.ForeignKey(
                    name: "FK_FormFields_FormEntity_FormId",
                    column: x => x.FormId,
                    principalSchema: "app",
                    principalTable: "FormEntity",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_FormEntity_SubscriptionId",
            schema: "app",
            table: "FormEntity",
            column: "SubscriptionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "FormFields",
            schema: "app");

        migrationBuilder.DropTable(
            name: "FormEntity",
            schema: "app");
    }
}

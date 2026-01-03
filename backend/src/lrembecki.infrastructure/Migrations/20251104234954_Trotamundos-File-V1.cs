using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations;

/// <inheritdoc />
public partial class TrotamundosFileV1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TrotamundosFile",
            schema: "app",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TrotamundosFile", x => x.Id);
                table.ForeignKey(
                    name: "FK_TrotamundosFile_Storage_StorageId",
                    column: x => x.StorageId,
                    principalSchema: "app",
                    principalTable: "Storage",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_TrotamundosFile_Subscription_SubscriptionId",
                    column: x => x.SubscriptionId,
                    principalSchema: "app",
                    principalTable: "Subscription",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TrotamundosFile_StorageId",
            schema: "app",
            table: "TrotamundosFile",
            column: "StorageId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_TrotamundosFile_SubscriptionId",
            schema: "app",
            table: "TrotamundosFile",
            column: "SubscriptionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TrotamundosFile",
            schema: "app");
    }
}

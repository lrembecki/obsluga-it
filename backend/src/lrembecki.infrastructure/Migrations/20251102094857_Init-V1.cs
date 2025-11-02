using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroup",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountPermissionGroup",
                schema: "app",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPermissionGroup", x => new { x.AccountId, x.PermissionGroupId });
                    table.ForeignKey(
                        name: "FK_AccountPermissionGroup_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "app",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountPermissionGroup_PermissionGroup_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "app",
                        principalTable: "PermissionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroupPermission",
                schema: "app",
                columns: table => new
                {
                    PermissionGrupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroupPermission", x => new { x.PermissionGrupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_PermissionGroupPermission_PermissionGroup_PermissionGrupId",
                        column: x => x.PermissionGrupId,
                        principalSchema: "app",
                        principalTable: "PermissionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionGroupPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "app",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSubscription",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSubscription_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "app",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountSubscription_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advantage",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advantage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advantage_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Highlight_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlobUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BlobPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storage_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountSubscriptionPermissionGroup",
                schema: "app",
                columns: table => new
                {
                    AccountSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubscriptionPermissionGroup", x => new { x.AccountSubscriptionId, x.PermissionGroupId });
                    table.ForeignKey(
                        name: "FK_AccountSubscriptionPermissionGroup_AccountSubscription_AccountSubscriptionId",
                        column: x => x.AccountSubscriptionId,
                        principalSchema: "app",
                        principalTable: "AccountSubscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountSubscriptionPermissionGroup_PermissionGroup_PermissionGroupId",
                        column: x => x.PermissionGroupId,
                        principalSchema: "app",
                        principalTable: "PermissionGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileStorage",
                schema: "app",
                columns: table => new
                {
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorage", x => x.StorageId);
                    table.ForeignKey(
                        name: "FK_FileStorage_Storage_StorageId",
                        column: x => x.StorageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageStorage",
                schema: "app",
                columns: table => new
                {
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Width = table.Column<long>(type: "bigint", nullable: false),
                    Height = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageStorage", x => x.StorageId);
                    table.ForeignKey(
                        name: "FK_ImageStorage_Storage_StorageId",
                        column: x => x.StorageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyalityProgram",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoyalityProgram", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoyalityProgram_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoyalityProgram_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripAdvantage",
                schema: "app",
                columns: table => new
                {
                    AdvantageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripAdvantage", x => new { x.AdvantageId, x.TripId });
                    table.ForeignKey(
                        name: "FK_TripAdvantage_Advantage_AdvantageId",
                        column: x => x.AdvantageId,
                        principalSchema: "app",
                        principalTable: "Advantage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripAdvantage_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripHighlight",
                schema: "app",
                columns: table => new
                {
                    HighlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripHighlight", x => new { x.HighlightId, x.TripId });
                    table.ForeignKey(
                        name: "FK_TripHighlight_Highlight_HighlightId",
                        column: x => x.HighlightId,
                        principalSchema: "app",
                        principalTable: "Highlight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripHighlight_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripImage",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripImage", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripImage_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripImage_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPaymentSchedule",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPaymentSchedule", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripPaymentSchedule_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPriceInclude",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Includes = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPriceInclude", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripPriceInclude_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripRequirement",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripRequirement", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripRequirement_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripSchedule",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSchedule", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripSchedule_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripSuggestedFlight",
                schema: "app",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSuggestedFlight", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripSuggestedFlight_Storage_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "app",
                        principalTable: "Storage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripSuggestedFlight_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "app",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_Email",
                schema: "app",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountPermissionGroup_PermissionGroupId",
                schema: "app",
                table: "AccountPermissionGroup",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubscription_AccountId",
                schema: "app",
                table: "AccountSubscription",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubscription_SubscriptionId",
                schema: "app",
                table: "AccountSubscription",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubscriptionPermissionGroup_PermissionGroupId",
                schema: "app",
                table: "AccountSubscriptionPermissionGroup",
                column: "PermissionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Advantage_SubscriptionId",
                schema: "app",
                table: "Advantage",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_SubscriptionId",
                schema: "app",
                table: "Highlight",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityProgram_ImageId",
                schema: "app",
                table: "LoyalityProgram",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityProgram_SubscriptionId",
                schema: "app",
                table: "LoyalityProgram",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_Name",
                schema: "app",
                table: "Permission",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroup_Name",
                schema: "app",
                table: "PermissionGroup",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionGroupPermission_PermissionId",
                schema: "app",
                table: "PermissionGroupPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_SubscriptionId",
                schema: "app",
                table: "Storage",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_Name",
                schema: "app",
                table: "Subscription",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_SubscriptionId",
                schema: "app",
                table: "Trip",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvantage_TripId",
                schema: "app",
                table: "TripAdvantage",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripHighlight_TripId",
                schema: "app",
                table: "TripHighlight",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripImage_ImageId",
                schema: "app",
                table: "TripImage",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripSuggestedFlight_ImageId",
                schema: "app",
                table: "TripSuggestedFlight",
                column: "ImageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountPermissionGroup",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AccountSubscriptionPermissionGroup",
                schema: "app");

            migrationBuilder.DropTable(
                name: "FileStorage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "ImageStorage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "LoyalityProgram",
                schema: "app");

            migrationBuilder.DropTable(
                name: "PermissionGroupPermission",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripAdvantage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripHighlight",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripImage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripPaymentSchedule",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripPriceInclude",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripRequirement",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripSchedule",
                schema: "app");

            migrationBuilder.DropTable(
                name: "TripSuggestedFlight",
                schema: "app");

            migrationBuilder.DropTable(
                name: "AccountSubscription",
                schema: "app");

            migrationBuilder.DropTable(
                name: "PermissionGroup",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Advantage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Highlight",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Storage",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Trip",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "app");
        }
    }
}

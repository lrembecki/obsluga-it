using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lrembecki.obsluga_it.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "oit");

            migrationBuilder.CreateTable(
                name: "Subscription",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
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
                name: "UnitOfMeasure",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actor",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actor_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advantage",
                schema: "oit",
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
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileGroup",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileGroup_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Highlight",
                schema: "oit",
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
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageBlob",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Width = table.Column<long>(type: "bigint", nullable: true),
                    Height = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BlobUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlobPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageBlob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageBlob_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trip",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionUser",
                schema: "oit",
                columns: table => new
                {
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionUser", x => new { x.SubscriptionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_Subscription_SubscriptionEntityId",
                        column: x => x.SubscriptionEntityId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionUser_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "oit",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ActorEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contact_Actor_ActorEntityId",
                        column: x => x.ActorEntityId,
                        principalSchema: "oit",
                        principalTable: "Actor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contact_Actor_ActorId",
                        column: x => x.ActorId,
                        principalSchema: "oit",
                        principalTable: "Actor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileBlob",
                schema: "oit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BlobUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BlobPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Size = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileBlob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileBlob_FileGroup_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "oit",
                        principalTable: "FileGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FileBlob_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoyalityProgram",
                schema: "oit",
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
                        name: "FK_LoyalityProgram_ImageBlob_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "oit",
                        principalTable: "ImageBlob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoyalityProgram_Subscription_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "oit",
                        principalTable: "Subscription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageBlobTag",
                schema: "oit",
                columns: table => new
                {
                    ImageBlobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageBlobTag", x => new { x.ImageBlobId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ImageBlobTag_ImageBlob_ImageBlobId",
                        column: x => x.ImageBlobId,
                        principalSchema: "oit",
                        principalTable: "ImageBlob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImageBlobTag_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "oit",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripAdvantage",
                schema: "oit",
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
                        principalSchema: "oit",
                        principalTable: "Advantage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripAdvantage_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripHighlight",
                schema: "oit",
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
                        principalSchema: "oit",
                        principalTable: "Highlight",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripHighlight_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripImage",
                schema: "oit",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    TripEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripImage", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripImage_ImageBlob_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "oit",
                        principalTable: "ImageBlob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripImage_Trip_TripEntityId",
                        column: x => x.TripEntityId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TripImage_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPaymentSchedule",
                schema: "oit",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    TripEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPaymentSchedule", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripPaymentSchedule_Trip_TripEntityId",
                        column: x => x.TripEntityId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TripPaymentSchedule_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripPriceInclude",
                schema: "oit",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Includes = table.Column<bool>(type: "bit", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    TripEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPriceInclude", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripPriceInclude_Trip_TripEntityId",
                        column: x => x.TripEntityId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TripPriceInclude_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripRequirement",
                schema: "oit",
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
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripSchedule",
                schema: "oit",
                columns: table => new
                {
                    TripId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    TripEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSchedule", x => new { x.TripId, x.Order });
                    table.ForeignKey(
                        name: "FK_TripSchedule_Trip_TripEntityId",
                        column: x => x.TripEntityId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TripSchedule_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripSuggestedFlight",
                schema: "oit",
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
                        name: "FK_TripSuggestedFlight_ImageBlob_ImageId",
                        column: x => x.ImageId,
                        principalSchema: "oit",
                        principalTable: "ImageBlob",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripSuggestedFlight_Trip_TripId",
                        column: x => x.TripId,
                        principalSchema: "oit",
                        principalTable: "Trip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actor_SubscriptionId",
                schema: "oit",
                table: "Actor",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Actor_UpdatedAt",
                schema: "oit",
                table: "Actor",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Advantage_SubscriptionId",
                schema: "oit",
                table: "Advantage",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Advantage_UpdatedAt",
                schema: "oit",
                table: "Advantage",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ActorEntityId",
                schema: "oit",
                table: "Contact",
                column: "ActorEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ActorId",
                schema: "oit",
                table: "Contact",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_SubscriptionId_Email",
                schema: "oit",
                table: "Contact",
                columns: new[] { "SubscriptionId", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UpdatedAt",
                schema: "oit",
                table: "Contact",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_FileBlob_GroupId",
                schema: "oit",
                table: "FileBlob",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FileBlob_SubscriptionId",
                schema: "oit",
                table: "FileBlob",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileBlob_UpdatedAt",
                schema: "oit",
                table: "FileBlob",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_FileGroup_SubscriptionId",
                schema: "oit",
                table: "FileGroup",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_FileGroup_UpdatedAt",
                schema: "oit",
                table: "FileGroup",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_SubscriptionId",
                schema: "oit",
                table: "Highlight",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Highlight_UpdatedAt",
                schema: "oit",
                table: "Highlight",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_ImageBlob_SubscriptionId",
                schema: "oit",
                table: "ImageBlob",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageBlob_UpdatedAt",
                schema: "oit",
                table: "ImageBlob",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_ImageBlobTag_TagId",
                schema: "oit",
                table: "ImageBlobTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityProgram_ImageId",
                schema: "oit",
                table: "LoyalityProgram",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityProgram_SubscriptionId",
                schema: "oit",
                table: "LoyalityProgram",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_LoyalityProgram_UpdatedAt",
                schema: "oit",
                table: "LoyalityProgram",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_Name",
                schema: "oit",
                table: "Subscription",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UpdatedAt",
                schema: "oit",
                table: "Subscription",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_SubscriptionEntityId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "SubscriptionEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_UpdatedAt",
                schema: "oit",
                table: "SubscriptionUser",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionUser_UserId",
                schema: "oit",
                table: "SubscriptionUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_SubscriptionId",
                schema: "oit",
                table: "Tag",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UpdatedAt",
                schema: "oit",
                table: "Tag",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Trip_SubscriptionId",
                schema: "oit",
                table: "Trip",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_UpdatedAt",
                schema: "oit",
                table: "Trip",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvantage_TripId",
                schema: "oit",
                table: "TripAdvantage",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripHighlight_TripId",
                schema: "oit",
                table: "TripHighlight",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_TripImage_ImageId",
                schema: "oit",
                table: "TripImage",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripImage_TripEntityId",
                schema: "oit",
                table: "TripImage",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPaymentSchedule_TripEntityId",
                schema: "oit",
                table: "TripPaymentSchedule",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripPriceInclude_TripEntityId",
                schema: "oit",
                table: "TripPriceInclude",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSchedule_TripEntityId",
                schema: "oit",
                table: "TripSchedule",
                column: "TripEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripSuggestedFlight_ImageId",
                schema: "oit",
                table: "TripSuggestedFlight",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasure_UpdatedAt",
                schema: "oit",
                table: "UnitOfMeasure",
                column: "UpdatedAt",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "oit",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UpdatedAt",
                schema: "oit",
                table: "User",
                column: "UpdatedAt",
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contact",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "FileBlob",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "ImageBlobTag",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "LoyalityProgram",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "SubscriptionUser",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripAdvantage",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripHighlight",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripImage",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripPaymentSchedule",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripPriceInclude",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripRequirement",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripSchedule",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "TripSuggestedFlight",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Actor",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "FileGroup",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "User",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Advantage",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Highlight",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "ImageBlob",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Trip",
                schema: "oit");

            migrationBuilder.DropTable(
                name: "Subscription",
                schema: "oit");
        }
    }
}

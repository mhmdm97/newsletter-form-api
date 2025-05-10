using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace newsletter_form_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunicationPreferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationPreferences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberCommunicationPreferences",
                columns: table => new
                {
                    CommunicationPreferencesId = table.Column<int>(type: "integer", nullable: false),
                    SubscribersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberCommunicationPreferences", x => new { x.CommunicationPreferencesId, x.SubscribersId });
                    table.ForeignKey(
                        name: "FK_SubscriberCommunicationPreferences_CommunicationPreferences~",
                        column: x => x.CommunicationPreferencesId,
                        principalTable: "CommunicationPreferences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberCommunicationPreferences_Subscribers_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberInterests",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "integer", nullable: false),
                    SubscribersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberInterests", x => new { x.InterestsId, x.SubscribersId });
                    table.ForeignKey(
                        name: "FK_SubscriberInterests_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriberInterests_Subscribers_SubscribersId",
                        column: x => x.SubscribersId,
                        principalTable: "Subscribers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CommunicationPreferences",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Tag", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(977), false, "Email", null },
                    { 2, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(978), false, "SMS", null }
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(882), false, "Houses", null },
                    { 2, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(885), false, "Apartments", null },
                    { 3, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(886), false, "Shared ownership", null },
                    { 4, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(886), false, "Rental", null },
                    { 5, new DateTime(2025, 5, 10, 12, 33, 48, 58, DateTimeKind.Utc).AddTicks(887), false, "Land sourcing", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberCommunicationPreferences_SubscribersId",
                table: "SubscriberCommunicationPreferences",
                column: "SubscribersId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberInterests_SubscribersId",
                table: "SubscriberInterests",
                column: "SubscribersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriberCommunicationPreferences");

            migrationBuilder.DropTable(
                name: "SubscriberInterests");

            migrationBuilder.DropTable(
                name: "CommunicationPreferences");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}

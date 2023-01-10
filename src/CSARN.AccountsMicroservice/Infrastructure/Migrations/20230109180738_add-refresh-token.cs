using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addrefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_AspNetUsers_AccountId",
                table: "Passports");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42a38618-01bc-47fd-b835-467f26311952"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6ab39199-daf9-46d8-bf25-aa60c5974edf"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6babd34f-cb40-4f87-a622-03881b589275"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("99cbf68a-f4e8-4222-8b3a-191ee30a9da3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bd007970-24a0-4bfd-bbf5-7ed4885a3f7c"));

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("032227bd-fe71-40c1-a820-1368fc9559d9"), "957b27ad-6127-4853-bff9-c4d4d2885824", "Citizen", "Citizen" },
                    { new Guid("190b7eb5-a2c5-466b-ac87-18746f19dfab"), "cb60c646-5579-4956-986f-78feb39ae445", "MinOfHealth", "MinOfHealth" },
                    { new Guid("833193b4-afdc-4bc8-8043-5b41b4bf532b"), "a8cdf953-8df4-446f-bdec-42a5f1f6b3f2", "MinOfInternalAffairs", "MinOfInternalAffairs" },
                    { new Guid("ac2644ec-b9d3-4634-a7da-e711db6c7310"), "a22cbf28-6774-4b4c-8410-f70db31df87d", "MinOfEmergencySituations", "MinOfEmergencySituations" },
                    { new Guid("ad73eb44-d408-4e3c-a6fd-522f9436cf4b"), "f89438ec-46ab-4014-8db9-d26c0d363957", "MunicipalRepresentative", "MunicipalRepresentative" },
                    { new Guid("ec0196bc-16a3-4e8d-a6cd-abcb2e193f40"), "fd45aab6-8319-4cdd-9d75-803f5cdd4c91", "Administrator", "Administrator" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshTokens",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_AspNetUsers_AccountId",
                table: "Passports",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passports_AspNetUsers_AccountId",
                table: "Passports");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("032227bd-fe71-40c1-a820-1368fc9559d9"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("190b7eb5-a2c5-466b-ac87-18746f19dfab"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("833193b4-afdc-4bc8-8043-5b41b4bf532b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac2644ec-b9d3-4634-a7da-e711db6c7310"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad73eb44-d408-4e3c-a6fd-522f9436cf4b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec0196bc-16a3-4e8d-a6cd-abcb2e193f40"));

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("42a38618-01bc-47fd-b835-467f26311952"), "84f5f69c-c586-4cf8-827a-c11df8bbdd99", "MinOfHealth", "MinOfHealth" },
                    { new Guid("6ab39199-daf9-46d8-bf25-aa60c5974edf"), "a3e1e17e-c688-4252-85a9-2bec7595b17b", "Administrator", "Administrator" },
                    { new Guid("6babd34f-cb40-4f87-a622-03881b589275"), "b290d8d4-15eb-4727-843e-f7dfdb2a1d13", "MinOfEmergencySituations", "MinOfEmergencySituations" },
                    { new Guid("99cbf68a-f4e8-4222-8b3a-191ee30a9da3"), "f7a3ea0e-354a-4af9-9747-51d2d8b08991", "MinOfInternalAffairs", "MinOfInternalAffairs" },
                    { new Guid("bd007970-24a0-4bfd-bbf5-7ed4885a3f7c"), "9fc5a561-9c54-4352-8a77-086abb286e28", "Citizen", "Citizen" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_AspNetUsers_AccountId",
                table: "Passports",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

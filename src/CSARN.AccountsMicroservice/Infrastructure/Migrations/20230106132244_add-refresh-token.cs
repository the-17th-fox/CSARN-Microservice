using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addrefreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Revoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2541278d-dfe8-4555-b2d2-a5ba05bebe21"), "457552ae-5a18-4fd6-a2cf-2774d62c0346", "MinOfEmergencySituations", "MinOfEmergencySituations" },
                    { new Guid("a2f4c175-f5e9-4d7f-be73-3a9426d6f96c"), "60e2779b-82b0-44e6-8ec4-97c7a78e7c9f", "Administrator", "Administrator" },
                    { new Guid("c62f4117-cb3a-4971-b36b-6bbb6b14e6ab"), "32947559-ed51-4160-a6c0-33c5ef77829e", "MinOfInternalAffairs", "MinOfInternalAffairs" },
                    { new Guid("e9678028-0333-4edf-a660-f5ccf1a5eef4"), "e904aee6-c66e-4266-aa47-0dc637986df6", "Citizen", "Citizen" },
                    { new Guid("f7cb59f5-ae28-4c37-bb1c-fc3ca7f8e92e"), "1d23bfcf-222d-470e-b60b-620fbba217bd", "MinOfHealth", "MinOfHealth" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                unique: true,
                filter: "[RefreshTokenId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Token");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RefreshTokens_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RefreshTokenId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2541278d-dfe8-4555-b2d2-a5ba05bebe21"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a2f4c175-f5e9-4d7f-be73-3a9426d6f96c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c62f4117-cb3a-4971-b36b-6bbb6b14e6ab"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e9678028-0333-4edf-a660-f5ccf1a5eef4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7cb59f5-ae28-4c37-bb1c-fc3ca7f8e92e"));

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                table: "AspNetUsers");

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
        }
    }
}

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
                name: "RefrToken",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefrToken.ExpAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RefrToken.IsRevoked",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3c35a26d-fbba-4070-8e51-da970296d0be"), "542dd129-63fa-4cf3-82bd-9d772276f228", "Administrator", "Administrator" },
                    { new Guid("60fe1c4e-d715-4f1e-ac7b-103c62f9c79d"), "55c24ccd-3055-4c86-b3de-8af901f4da30", "MinOfEmergencySituations", "MinOfEmergencySituations" },
                    { new Guid("8ed1fa25-c70e-4595-a398-7c5e6a4e92dd"), "1662e5aa-922e-4fe9-ba19-d28b8caac6af", "MinOfHealth", "MinOfHealth" },
                    { new Guid("96f0fe0c-126c-4041-9a27-a1fe203ddd5c"), "8b556512-42c2-4d68-bcca-3bf9fab10b0a", "Citizen", "Citizen" },
                    { new Guid("e116b3eb-de4a-450a-bf77-29b07615c629"), "cd88e363-b4f6-49df-abb6-6cda20cfd05d", "MinOfInternalAffairs", "MinOfInternalAffairs" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3c35a26d-fbba-4070-8e51-da970296d0be"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("60fe1c4e-d715-4f1e-ac7b-103c62f9c79d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8ed1fa25-c70e-4595-a398-7c5e6a4e92dd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("96f0fe0c-126c-4041-9a27-a1fe203ddd5c"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e116b3eb-de4a-450a-bf77-29b07615c629"));

            migrationBuilder.DropColumn(
                name: "RefrToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefrToken.ExpAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefrToken.IsRevoked",
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

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKT.Website.Migrations
{
    /// <inheritdoc />
    public partial class newfeildsforperson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "64578191-6386-45cf-bafd-97cf8bf57c20" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64578191-6386-45cf-bafd-97cf8bf57c20");

            migrationBuilder.AddColumn<string>(
                name: "PersonBudget",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonMessage",
                table: "Persons",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonRequestedService",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonTimeFrame",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e9961168-598e-4f2e-9adf-69f2f24b1f90", 0, "829274b9-c5f7-40bd-bedc-04739cc0ac1a", "SuperAdmin@gmail.com", true, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEEirhnn/SexK6O6WOcy72cRJZNyp0lv+TcgD0/sYIcUdmFRMYHA70dHe8JRgMF0fBA==", null, false, "ae9f863f-2ca1-43cd-8000-c715cdb3aab7", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "e9961168-598e-4f2e-9adf-69f2f24b1f90" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "e9961168-598e-4f2e-9adf-69f2f24b1f90" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e9961168-598e-4f2e-9adf-69f2f24b1f90");

            migrationBuilder.DropColumn(
                name: "PersonBudget",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonMessage",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonRequestedService",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonTimeFrame",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Persons");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64578191-6386-45cf-bafd-97cf8bf57c20", 0, "fffc611b-a90c-46e8-a075-9bf071d3b0fe", "SuperAdmin@gmail.com", true, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEM9a4DqN4hTmEngLCfpZD0b6xQEo4AV9ZHSX1VVNXBgtF8OY6GfHJyR2EyuzX+XRvw==", null, false, "974d4c74-2f55-4442-8cc8-fdbffb68abdd", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "64578191-6386-45cf-bafd-97cf8bf57c20" });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MKT.Website.Migrations
{
    /// <inheritdoc />
    public partial class identityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "b92f3cc3-bd1c-48e3-a92b-a8b8c8a67761" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b92f3cc3-bd1c-48e3-a92b-a8b8c8a67761");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "64578191-6386-45cf-bafd-97cf8bf57c20", 0, "fffc611b-a90c-46e8-a075-9bf071d3b0fe", "SuperAdmin@gmail.com", true, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEM9a4DqN4hTmEngLCfpZD0b6xQEo4AV9ZHSX1VVNXBgtF8OY6GfHJyR2EyuzX+XRvw==", null, false, "974d4c74-2f55-4442-8cc8-fdbffb68abdd", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "64578191-6386-45cf-bafd-97cf8bf57c20" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "64578191-6386-45cf-bafd-97cf8bf57c20" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "64578191-6386-45cf-bafd-97cf8bf57c20");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b92f3cc3-bd1c-48e3-a92b-a8b8c8a67761", 0, "70917ae2-e830-49e7-b737-7c4119820c85", "SuperAdmin@gmail.com", true, false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEAfOfAzkom6X+N+YlZgJo+a9KI7rhNVbRuY95yM/Dvz51Ia24jsCcqvCsaup2vfWdg==", null, false, "3fd7dc9e-08d2-4ab2-9ee4-d2c2c960495f", false, "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f2c3f0d1-77e5-4fc2-bb59-b6b811380be7", "b92f3cc3-bd1c-48e3-a92b-a8b8c8a67761" });
        }
    }
}

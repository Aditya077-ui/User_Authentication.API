using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedROles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a82891a8-015b-4e44-b6cb-a51935b8fce0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b64ed0f6-b469-46ad-97ac-c18d5129d115");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "242cc943-a59e-444b-9de2-34b67053c645", "242cc943-a59e-444b-9de2-34b67053c645", "User", "USER" },
                    { "a32a4c45-5184-4ca3-8518-96e1ba46cabb", "a32a4c45-5184-4ca3-8518-96e1ba46cabb", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "242cc943-a59e-444b-9de2-34b67053c645");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a32a4c45-5184-4ca3-8518-96e1ba46cabb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a82891a8-015b-4e44-b6cb-a51935b8fce0", "a82891a8-015b-4e44-b6cb-a51935b8fce0", "User", "USER" },
                    { "b64ed0f6-b469-46ad-97ac-c18d5129d115", "b64ed0f6-b469-46ad-97ac-c18d5129d115", "Admin", "ADMIN" }
                });
        }
    }
}

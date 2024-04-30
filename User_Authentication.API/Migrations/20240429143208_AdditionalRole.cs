using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5216cbd0-9042-4b21-9f92-791aa9e75358");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7ce0375-20a5-4526-8c75-5bc9476f0fc9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a82891a8-015b-4e44-b6cb-a51935b8fce0", "a82891a8-015b-4e44-b6cb-a51935b8fce0", "User", "USER" },
                    { "b64ed0f6-b469-46ad-97ac-c18d5129d115", "b64ed0f6-b469-46ad-97ac-c18d5129d115", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "5216cbd0-9042-4b21-9f92-791aa9e75358", "5216cbd0-9042-4b21-9f92-791aa9e75358", "Admin", "ADMIN" },
                    { "e7ce0375-20a5-4526-8c75-5bc9476f0fc9", "e7ce0375-20a5-4526-8c75-5bc9476f0fc9", "User", "USER" }
                });
        }
    }
}

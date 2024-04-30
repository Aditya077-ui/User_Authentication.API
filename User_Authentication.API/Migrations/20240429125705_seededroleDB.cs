using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User_Authentication.API.Migrations
{
    /// <inheritdoc />
    public partial class seededroleDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5216cbd0-9042-4b21-9f92-791aa9e75358", "5216cbd0-9042-4b21-9f92-791aa9e75358", "Admin", "ADMIN" },
                    { "e7ce0375-20a5-4526-8c75-5bc9476f0fc9", "e7ce0375-20a5-4526-8c75-5bc9476f0fc9", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5216cbd0-9042-4b21-9f92-791aa9e75358");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7ce0375-20a5-4526-8c75-5bc9476f0fc9");
        }
    }
}

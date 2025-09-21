using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce_Project.Migrations
{
    /// <inheritdoc />
    public partial class seedsdataIdentityRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ae80691-51ad-4d5c-b04e-aee2cdbb1e1a", null, "User", "USER" },
                    { "c7c5cf8b-b04d-43d0-bbb8-3c8e413f169c", null, "Admine", "ADMINE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ae80691-51ad-4d5c-b04e-aee2cdbb1e1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7c5cf8b-b04d-43d0-bbb8-3c8e413f169c");
        }
    }
}

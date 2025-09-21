using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce_Project.Migrations
{
    /// <inheritdoc />
    public partial class add_pricetoProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ae80691-51ad-4d5c-b04e-aee2cdbb1e1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7c5cf8b-b04d-43d0-bbb8-3c8e413f169c");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35bc972c-3996-49bb-bb9d-2164cf93f719", null, "User", "USER" },
                    { "6bb568bd-2965-4b57-ba94-842860a0e994", null, "Admine", "ADMINE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35bc972c-3996-49bb-bb9d-2164cf93f719");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6bb568bd-2965-4b57-ba94-842860a0e994");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ae80691-51ad-4d5c-b04e-aee2cdbb1e1a", null, "User", "USER" },
                    { "c7c5cf8b-b04d-43d0-bbb8-3c8e413f169c", null, "Admine", "ADMINE" }
                });
        }
    }
}

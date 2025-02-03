using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace stocks.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fac8559-31f6-481b-9f5f-7f71f76a44b0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9eb595f3-4b82-4366-8909-b413c373537c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5", "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5", "Admin", "ADMIN" },
                    { "57dec208-734e-4ca6-a270-d05687f4df81", "57dec208-734e-4ca6-a270-d05687f4df81", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57dec208-734e-4ca6-a270-d05687f4df81");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7fac8559-31f6-481b-9f5f-7f71f76a44b0", "57dec208-734e-4ca6-a270-d05687f4df81", "User", "USER" },
                    { "9eb595f3-4b82-4366-8909-b413c373537c", "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5", "Admin", "ADMIN" }
                });
        }
    }
}

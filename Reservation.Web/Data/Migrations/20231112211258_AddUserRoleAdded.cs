using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Reservation.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8466d359-42c7-4f57-bc23-a94d4d672a06", null, "Admin", "ADMIN" },
                    { "8bac8a00-3b5f-40bc-89cc-c19b77157937", null, "Editor", "EDITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8466d359-42c7-4f57-bc23-a94d4d672a06");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bac8a00-3b5f-40bc-89cc-c19b77157937");
        }
    }
}

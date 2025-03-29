using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebHost.Migrations
{
    /// <inheritdoc />
    public partial class Fixtypos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "46fd8eb9-40b1-4fac-9ec4-8695f7962f40");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "b526a428-b473-4936-b905-39ca24ad3de4");

            migrationBuilder.RenameColumn(
                name: "refresh_token_value",
                table: "refresh_tokens",
                newName: "token");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "04a996f3-0343-4f0d-a673-e95d5cd5abf6", "instructor" },
                    { "230819b4-c682-42c2-b2ef-18eb5005c6cb", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "04a996f3-0343-4f0d-a673-e95d5cd5abf6");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "230819b4-c682-42c2-b2ef-18eb5005c6cb");

            migrationBuilder.RenameColumn(
                name: "token",
                table: "refresh_tokens",
                newName: "refresh_token_value");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "46fd8eb9-40b1-4fac-9ec4-8695f7962f40", "instructor" },
                    { "b526a428-b473-4936-b905-39ca24ad3de4", "user" }
                });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebHost.Migrations
{
    /// <inheritdoc />
    public partial class Add_missing_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "73a023da-5442-432c-8db0-c6165b772e28");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "9a75408f-78a2-48d5-8e92-c77213feed05");

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "46fd8eb9-40b1-4fac-9ec4-8695f7962f40", "instructor" },
                    { "b526a428-b473-4936-b905-39ca24ad3de4", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "46fd8eb9-40b1-4fac-9ec4-8695f7962f40");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "b526a428-b473-4936-b905-39ca24ad3de4");

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "73a023da-5442-432c-8db0-c6165b772e28", "instructor" },
                    { "9a75408f-78a2-48d5-8e92-c77213feed05", "user" }
                });
        }
    }
}

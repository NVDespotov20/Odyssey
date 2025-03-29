using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebHost.Migrations
{
    /// <inheritdoc />
    public partial class Makeaboutnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "04a996f3-0343-4f0d-a673-e95d5cd5abf6");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "230819b4-c682-42c2-b2ef-18eb5005c6cb");

            migrationBuilder.AlterColumn<string>(
                name: "about_me",
                table: "users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "c354aaeb-b6f9-422f-b8a0-887fa4ea8940", "instructor" },
                    { "ed863c51-9132-438e-97ff-2e2fdd360bb6", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "c354aaeb-b6f9-422f-b8a0-887fa4ea8940");

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: "ed863c51-9132-438e-97ff-2e2fdd360bb6");

            migrationBuilder.AlterColumn<string>(
                name: "about_me",
                table: "users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "04a996f3-0343-4f0d-a673-e95d5cd5abf6", "instructor" },
                    { "230819b4-c682-42c2-b2ef-18eb5005c6cb", "user" }
                });
        }
    }
}

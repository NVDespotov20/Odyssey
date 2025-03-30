using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebHost.Migrations
{
    /// <inheritdoc />
    public partial class Addonetomanyforusersandacademies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademyId",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademyId",
                table: "AspNetUsers",
                column: "AcademyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Academies_AcademyId",
                table: "AspNetUsers",
                column: "AcademyId",
                principalTable: "Academies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Academies_AcademyId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AcademyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AcademyId",
                table: "AspNetUsers");
        }
    }
}

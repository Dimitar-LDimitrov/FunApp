using Microsoft.EntityFrameworkCore.Migrations;

namespace FunApp.Data.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserJokes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCategories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserJokes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

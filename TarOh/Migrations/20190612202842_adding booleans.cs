using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class addingbooleans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CardDirection",
                table: "SavedSpread",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CardDirection",
                table: "Card",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardDirection",
                table: "SavedSpread");

            migrationBuilder.DropColumn(
                name: "CardDirection",
                table: "Card");
        }
    }
}

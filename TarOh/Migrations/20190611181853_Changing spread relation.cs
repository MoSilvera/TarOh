using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class Changingspreadrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Spread_SpreadId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_SpreadId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "SpreadId",
                table: "Card");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpreadId",
                table: "Card",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Card_SpreadId",
                table: "Card",
                column: "SpreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Spread_SpreadId",
                table: "Card",
                column: "SpreadId",
                principalTable: "Spread",
                principalColumn: "SpreadId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

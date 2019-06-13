using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class chaningcolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedSpread_OrdinalPosition_SavedSpread_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.DropIndex(
                name: "IX_SavedSpread_SavedSpread_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.DropColumn(
                name: "SavedSpread_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.CreateIndex(
                name: "IX_SavedSpread_OrdinalPositionId",
                table: "SavedSpread",
                column: "OrdinalPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedSpread_OrdinalPosition_OrdinalPositionId",
                table: "SavedSpread",
                column: "OrdinalPositionId",
                principalTable: "OrdinalPosition",
                principalColumn: "OrdinalPositionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedSpread_OrdinalPosition_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.DropIndex(
                name: "IX_SavedSpread_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.AddColumn<int>(
                name: "SavedSpread_OrdinalPositionId",
                table: "SavedSpread",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedSpread_SavedSpread_OrdinalPositionId",
                table: "SavedSpread",
                column: "SavedSpread_OrdinalPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedSpread_OrdinalPosition_SavedSpread_OrdinalPositionId",
                table: "SavedSpread",
                column: "SavedSpread_OrdinalPositionId",
                principalTable: "OrdinalPosition",
                principalColumn: "OrdinalPositionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

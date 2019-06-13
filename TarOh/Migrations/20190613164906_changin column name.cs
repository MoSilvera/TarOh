using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class changincolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedSpread_OrdinalPosition_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.DropIndex(
                name: "IX_SavedSpread_OrdinalPositionId",
                table: "SavedSpread");

            migrationBuilder.DropColumn(
                name: "OrdinalId",
                table: "SavedSpread");

            migrationBuilder.AlterColumn<int>(
                name: "OrdinalPositionId",
                table: "SavedSpread",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "OrdinalPositionId",
                table: "SavedSpread",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "OrdinalId",
                table: "SavedSpread",
                nullable: false,
                defaultValue: 0);

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
                onDelete: ReferentialAction.Restrict);
        }
    }
}

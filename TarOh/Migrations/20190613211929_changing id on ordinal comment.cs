using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class changingidonordinalcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinalComment_Card_CardId",
                table: "OrdinalComment");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinalComment_OrdinalPosition_OrdinalPositionId",
                table: "OrdinalComment");

            migrationBuilder.DropIndex(
                name: "IX_OrdinalComment_CardId",
                table: "OrdinalComment");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "OrdinalComment");

            migrationBuilder.AlterColumn<int>(
                name: "OrdinalPositionId",
                table: "OrdinalComment",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinalComment_OrdinalPosition_OrdinalPositionId",
                table: "OrdinalComment",
                column: "OrdinalPositionId",
                principalTable: "OrdinalPosition",
                principalColumn: "OrdinalPositionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinalComment_OrdinalPosition_OrdinalPositionId",
                table: "OrdinalComment");

            migrationBuilder.AlterColumn<int>(
                name: "OrdinalPositionId",
                table: "OrdinalComment",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "OrdinalComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrdinalComment_CardId",
                table: "OrdinalComment",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinalComment_Card_CardId",
                table: "OrdinalComment",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinalComment_OrdinalPosition_OrdinalPositionId",
                table: "OrdinalComment",
                column: "OrdinalPositionId",
                principalTable: "OrdinalPosition",
                principalColumn: "OrdinalPositionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

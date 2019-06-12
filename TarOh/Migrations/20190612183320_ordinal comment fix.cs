using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class ordinalcommentfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "OrdinalComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "OrdinalComment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrdinalComment",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeckId",
                table: "Card",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrdinalComment_CardId",
                table: "OrdinalComment",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinalComment_UserId",
                table: "OrdinalComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinalComment_Card_CardId",
                table: "OrdinalComment",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinalComment_AspNetUsers_UserId",
                table: "OrdinalComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinalComment_Card_CardId",
                table: "OrdinalComment");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinalComment_AspNetUsers_UserId",
                table: "OrdinalComment");

            migrationBuilder.DropIndex(
                name: "IX_OrdinalComment_CardId",
                table: "OrdinalComment");

            migrationBuilder.DropIndex(
                name: "IX_OrdinalComment_UserId",
                table: "OrdinalComment");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "OrdinalComment");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "OrdinalComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrdinalComment");

            migrationBuilder.AlterColumn<int>(
                name: "DeckId",
                table: "Card",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Deck_DeckId",
                table: "Card",
                column: "DeckId",
                principalTable: "Deck",
                principalColumn: "DeckId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class ChangingCardTypeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardType_CardTypeId",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Card");

            migrationBuilder.AlterColumn<int>(
                name: "CardTypeId",
                table: "Card",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_CardType_CardTypeId",
                table: "Card",
                column: "CardTypeId",
                principalTable: "CardType",
                principalColumn: "CardTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_CardType_CardTypeId",
                table: "Card");

            migrationBuilder.AlterColumn<int>(
                name: "CardTypeId",
                table: "Card",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Card",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_CardType_CardTypeId",
                table: "Card",
                column: "CardTypeId",
                principalTable: "CardType",
                principalColumn: "CardTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

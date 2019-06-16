using Microsoft.EntityFrameworkCore.Migrations;

namespace TarOh.Migrations
{
    public partial class spreadcomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrdinalPositionId",
                table: "SpreadComment",
                newName: "SpreadCommentId");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "SpreadComment",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpreadId",
                table: "SpreadComment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SpreadComment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpreadComment_SpreadId",
                table: "SpreadComment",
                column: "SpreadId");

            migrationBuilder.CreateIndex(
                name: "IX_SpreadComment_UserId",
                table: "SpreadComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpreadComment_Spread_SpreadId",
                table: "SpreadComment",
                column: "SpreadId",
                principalTable: "Spread",
                principalColumn: "SpreadId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpreadComment_AspNetUsers_UserId",
                table: "SpreadComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpreadComment_Spread_SpreadId",
                table: "SpreadComment");

            migrationBuilder.DropForeignKey(
                name: "FK_SpreadComment_AspNetUsers_UserId",
                table: "SpreadComment");

            migrationBuilder.DropIndex(
                name: "IX_SpreadComment_SpreadId",
                table: "SpreadComment");

            migrationBuilder.DropIndex(
                name: "IX_SpreadComment_UserId",
                table: "SpreadComment");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "SpreadComment");

            migrationBuilder.DropColumn(
                name: "SpreadId",
                table: "SpreadComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SpreadComment");

            migrationBuilder.RenameColumn(
                name: "SpreadCommentId",
                table: "SpreadComment",
                newName: "OrdinalPositionId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Picturesque.DB.Migrations
{
    public partial class EditedUserGameScoreRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameScores",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameScores_UserId",
                table: "GameScores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameScores_AspNetUsers_UserId",
                table: "GameScores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameScores_AspNetUsers_UserId",
                table: "GameScores");

            migrationBuilder.DropIndex(
                name: "IX_GameScores_UserId",
                table: "GameScores");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "GameScores",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Picturesque.DB.Migrations
{
    public partial class EditedUserStatisticsRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserStatistics",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStatistics_AspNetUsers_UserId",
                table: "UserStatistics",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStatistics_AspNetUsers_UserId",
                table: "UserStatistics");

            migrationBuilder.DropIndex(
                name: "IX_UserStatistics_UserId",
                table: "UserStatistics");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserStatistics",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

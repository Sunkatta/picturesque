using Microsoft.EntityFrameworkCore.Migrations;

namespace Picturesque.DB.Migrations
{
    public partial class AddedUserStatisticsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GamesWon = table.Column<int>(nullable: false),
                    GamesLost = table.Column<int>(nullable: false),
                    TotalScore = table.Column<int>(nullable: false),
                    TotalNumberOfMistakes = table.Column<int>(nullable: false),
                    TotalPlaytime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistics");
        }
    }
}

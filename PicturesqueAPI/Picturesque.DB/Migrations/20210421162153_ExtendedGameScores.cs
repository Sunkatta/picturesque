using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Picturesque.DB.Migrations
{
    public partial class ExtendedGameScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedInSeconds",
                table: "GameScores",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "GameScores",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsHelpUsed",
                table: "GameScores",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMistakes",
                table: "GameScores",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedInSeconds",
                table: "GameScores");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "GameScores");

            migrationBuilder.DropColumn(
                name: "IsHelpUsed",
                table: "GameScores");

            migrationBuilder.DropColumn(
                name: "NumberOfMistakes",
                table: "GameScores");
        }
    }
}

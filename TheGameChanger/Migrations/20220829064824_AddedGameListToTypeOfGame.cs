using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGameChanger.Migrations
{
    public partial class AddedGameListToTypeOfGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_TypeOfGameId",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TypeOfGameId",
                table: "Games",
                column: "TypeOfGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_TypeOfGameId",
                table: "Games");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TypeOfGameId",
                table: "Games",
                column: "TypeOfGameId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGameChanger.Migrations
{
    public partial class ChangeTypeOnTypeOfGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Types_TypeId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Games",
                newName: "TypeOfGameId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_TypeId",
                table: "Games",
                newName: "IX_Games_TypeOfGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Types_TypeOfGameId",
                table: "Games",
                column: "TypeOfGameId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Types_TypeOfGameId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "TypeOfGameId",
                table: "Games",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_TypeOfGameId",
                table: "Games",
                newName: "IX_Games_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Types_TypeId",
                table: "Games",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

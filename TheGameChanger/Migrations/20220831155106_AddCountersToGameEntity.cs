using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheGameChanger.Migrations
{
    public partial class AddCountersToGameEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NegativeCounter",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositiveCounter",
                table: "Games",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NegativeCounter",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PositiveCounter",
                table: "Games");
        }
    }
}

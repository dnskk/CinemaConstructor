using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaConstructor.Database.Migrations
{
    public partial class AddedAccentColors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccentColorFirst",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccentColorSecond",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccentColorFirst",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AccentColorSecond",
                table: "Companies");
        }
    }
}

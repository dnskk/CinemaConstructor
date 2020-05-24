using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaConstructor.Database.Migrations
{
    public partial class AddedWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YandexWallet",
                table: "Companies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YandexWallet",
                table: "Companies");
        }
    }
}

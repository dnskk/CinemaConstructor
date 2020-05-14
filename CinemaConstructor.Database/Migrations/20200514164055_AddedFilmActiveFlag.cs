using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaConstructor.Database.Migrations
{
    public partial class AddedFilmActiveFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Films",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Films");
        }
    }
}

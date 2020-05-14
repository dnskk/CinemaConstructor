using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaConstructor.Database.Migrations
{
    public partial class AddedFilmToUserSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrentFilmId",
                table: "UserSessions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CurrentFilmSessionId",
                table: "UserSessions",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentFilmId",
                table: "UserSessions");

            migrationBuilder.DropColumn(
                name: "CurrentFilmSessionId",
                table: "UserSessions");
        }
    }
}

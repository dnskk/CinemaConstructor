using Microsoft.EntityFrameworkCore.Migrations;

namespace CinemaConstructor.Database.Migrations
{
    public partial class AddedTickets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Row = table.Column<long>(nullable: false),
                    Column = table.Column<long>(nullable: false),
                    FilmSessionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_FilmSessions_FilmSessionId",
                        column: x => x.FilmSessionId,
                        principalTable: "FilmSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FilmSessionId",
                table: "Tickets",
                column: "FilmSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}

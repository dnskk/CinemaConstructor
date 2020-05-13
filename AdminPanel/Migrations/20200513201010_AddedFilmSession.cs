using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Migrations
{
    public partial class AddedFilmSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmSessions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmId = table.Column<long>(nullable: true),
                    HallId = table.Column<long>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Price = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmSessions_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilmSessions_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmSessions_FilmId",
                table: "FilmSessions",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmSessions_HallId",
                table: "FilmSessions",
                column: "HallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmSessions");
        }
    }
}

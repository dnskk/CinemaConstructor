using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Migrations
{
    public partial class AddHall : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Rows = table.Column<int>(nullable: false),
                    Columns = table.Column<int>(nullable: false),
                    Is3D = table.Column<bool>(nullable: false),
                    IsImax = table.Column<bool>(nullable: false),
                    HallTableJson = table.Column<string>(nullable: true),
                    CinemaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halls_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CinemaId",
                table: "Halls",
                column: "CinemaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Halls");
        }
    }
}

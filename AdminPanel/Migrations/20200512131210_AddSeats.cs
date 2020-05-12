using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Migrations
{
    public partial class AddSeats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "Halls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Seats",
                table: "Halls");
        }
    }
}

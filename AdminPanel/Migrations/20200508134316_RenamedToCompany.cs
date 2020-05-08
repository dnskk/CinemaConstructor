using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Migrations
{
    public partial class RenamedToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_CinemaCompanies_CinemaCompanyId",
                table: "Cinemas");

            migrationBuilder.DropTable(
                name: "CinemaCompanies");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_CinemaCompanyId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "CinemaCompanyId",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Cinemas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CompanyId",
                table: "Cinemas",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_Companies_CompanyId",
                table: "Cinemas",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cinemas_Companies_CompanyId",
                table: "Cinemas");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Cinemas_CompanyId",
                table: "Cinemas");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Cinemas");

            migrationBuilder.AddColumn<int>(
                name: "CinemaCompanyId",
                table: "Cinemas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CinemaCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CinemaCompanies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cinemas_CinemaCompanyId",
                table: "Cinemas",
                column: "CinemaCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cinemas_CinemaCompanies_CinemaCompanyId",
                table: "Cinemas",
                column: "CinemaCompanyId",
                principalTable: "CinemaCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

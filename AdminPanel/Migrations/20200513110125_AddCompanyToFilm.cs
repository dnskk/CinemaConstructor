using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Migrations
{
    public partial class AddCompanyToFilm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                table: "Films",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Films_CompanyId",
                table: "Films",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Companies_CompanyId",
                table: "Films",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Companies_CompanyId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_CompanyId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Films");
        }
    }
}

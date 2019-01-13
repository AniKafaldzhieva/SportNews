using Microsoft.EntityFrameworkCore.Migrations;

namespace SportNews.Web.Data.Migrations
{
    public partial class AddIDColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeagueID",
                table: "Leagues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Countries",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueID",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Countries");
        }
    }
}

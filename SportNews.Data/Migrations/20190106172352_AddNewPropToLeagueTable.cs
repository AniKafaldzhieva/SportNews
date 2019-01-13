using Microsoft.EntityFrameworkCore.Migrations;

namespace SportNews.Web.Data.Migrations
{
    public partial class AddNewPropToLeagueTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryID",
                table: "Leagues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_CountryID",
                table: "Leagues",
                column: "CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Leagues_Countries_CountryID",
                table: "Leagues",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leagues_Countries_CountryID",
                table: "Leagues");

            migrationBuilder.DropIndex(
                name: "IX_Leagues_CountryID",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "Leagues");
        }
    }
}

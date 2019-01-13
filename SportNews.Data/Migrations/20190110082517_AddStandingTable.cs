using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportNews.Web.Data.Migrations
{
    public partial class AddStandingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Standings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LeagueId = table.Column<int>(nullable: false),
                    LeagueKey = table.Column<string>(nullable: true),
                    LeagueSeason = table.Column<string>(nullable: true),
                    LeagueRound = table.Column<string>(nullable: true),
                    Place = table.Column<string>(nullable: true),
                    PlaceType = table.Column<string>(nullable: true),
                    TeamID = table.Column<int>(nullable: false),
                    TeamKey = table.Column<string>(nullable: true),
                    GamesPlayed = table.Column<string>(nullable: true),
                    Wins = table.Column<string>(nullable: true),
                    Draw = table.Column<string>(nullable: true),
                    Losts = table.Column<string>(nullable: true),
                    GoalsFor = table.Column<string>(nullable: true),
                    GoalsAgainst = table.Column<string>(nullable: true),
                    GoalDifference = table.Column<string>(nullable: true),
                    Points = table.Column<string>(nullable: true),
                    StandingUpdated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Standings_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Standings_Teams_TeamID",
                        column: x => x.TeamID,
                        principalTable: "Teams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Standings_LeagueId",
                table: "Standings",
                column: "LeagueId");

            migrationBuilder.CreateIndex(
                name: "IX_Standings_TeamID",
                table: "Standings",
                column: "TeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Standings");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportNews.Web.Data.Migrations
{
    public partial class RecreateTables5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            
            //migrationBuilder.CreateTable(
            //    name: "Leagues",
            //    columns: table => new
            //    {
            //        ID = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        LeagueID = table.Column<int>(nullable: false),
            //        Name = table.Column<string>(nullable: true),
            //        CountryID = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Leagues", x => x.ID);
            //        table.ForeignKey(
            //            name: "FK_Leagues_Countries_CountryID",
            //            column: x => x.CountryID,
            //            principalTable: "Countries",
            //            principalColumn: "CountryID",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            
            
            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryID",
                table: "Countries",
                column: "CountryID",
                unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Leagues_CountryID",
            //    table: "Leagues",
            //    column: "CountryID");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "Leagues");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

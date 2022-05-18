using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    public partial class AddedMatchInverseProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_MatchId",
                table: "PlayerOrMatchResult");

            migrationBuilder.DropIndex(
                name: "IX_PlayerOrMatchResult_MatchId",
                table: "PlayerOrMatchResult");

            migrationBuilder.AddColumn<int>(
                name: "MatchRequiringResultId",
                table: "Matches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchRequiringResultId",
                table: "Matches",
                column: "MatchRequiringResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayerOrMatchResult_MatchRequiringResultId",
                table: "Matches",
                column: "MatchRequiringResultId",
                principalTable: "PlayerOrMatchResult",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayerOrMatchResult_MatchRequiringResultId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_MatchRequiringResultId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchRequiringResultId",
                table: "Matches");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerOrMatchResult_MatchId",
                table: "PlayerOrMatchResult",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_MatchId",
                table: "PlayerOrMatchResult",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}

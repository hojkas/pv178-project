using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    public partial class renamed_property_of_match : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayerOrMatchResult_MatchRequiringResultId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_OriginalMatchId",
                table: "PlayerOrMatchResult");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResult_Players_PlayerId",
                table: "PlayerOrMatchResult");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerOrMatchResult",
                table: "PlayerOrMatchResult");

            migrationBuilder.RenameTable(
                name: "PlayerOrMatchResult",
                newName: "PlayerOrMatchResults");

            migrationBuilder.RenameColumn(
                name: "MatchRequiringResultId",
                table: "Matches",
                newName: "PlayerRequiringResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_MatchRequiringResultId",
                table: "Matches",
                newName: "IX_Matches_PlayerRequiringResultId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOrMatchResult_PlayerId",
                table: "PlayerOrMatchResults",
                newName: "IX_PlayerOrMatchResults_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOrMatchResult_OriginalMatchId",
                table: "PlayerOrMatchResults",
                newName: "IX_PlayerOrMatchResults_OriginalMatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerOrMatchResults",
                table: "PlayerOrMatchResults",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayerOrMatchResults_PlayerRequiringResultId",
                table: "Matches",
                column: "PlayerRequiringResultId",
                principalTable: "PlayerOrMatchResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResults_Matches_OriginalMatchId",
                table: "PlayerOrMatchResults",
                column: "OriginalMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResults_Players_PlayerId",
                table: "PlayerOrMatchResults",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_PlayerOrMatchResults_PlayerRequiringResultId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResults_Matches_OriginalMatchId",
                table: "PlayerOrMatchResults");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResults_Players_PlayerId",
                table: "PlayerOrMatchResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerOrMatchResults",
                table: "PlayerOrMatchResults");

            migrationBuilder.RenameTable(
                name: "PlayerOrMatchResults",
                newName: "PlayerOrMatchResult");

            migrationBuilder.RenameColumn(
                name: "PlayerRequiringResultId",
                table: "Matches",
                newName: "MatchRequiringResultId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_PlayerRequiringResultId",
                table: "Matches",
                newName: "IX_Matches_MatchRequiringResultId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOrMatchResults_PlayerId",
                table: "PlayerOrMatchResult",
                newName: "IX_PlayerOrMatchResult_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayerOrMatchResults_OriginalMatchId",
                table: "PlayerOrMatchResult",
                newName: "IX_PlayerOrMatchResult_OriginalMatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerOrMatchResult",
                table: "PlayerOrMatchResult",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_PlayerOrMatchResult_MatchRequiringResultId",
                table: "Matches",
                column: "MatchRequiringResultId",
                principalTable: "PlayerOrMatchResult",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_OriginalMatchId",
                table: "PlayerOrMatchResult",
                column: "OriginalMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResult_Players_PlayerId",
                table: "PlayerOrMatchResult",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }
    }
}

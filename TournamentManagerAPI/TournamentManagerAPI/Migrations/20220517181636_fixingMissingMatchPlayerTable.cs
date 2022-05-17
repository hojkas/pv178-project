using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    public partial class fixingMissingMatchPlayerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerOrMatchResult_OriginalMatchId",
                table: "PlayerOrMatchResult",
                column: "OriginalMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_OriginalMatchId",
                table: "PlayerOrMatchResult",
                column: "OriginalMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerOrMatchResult_Matches_OriginalMatchId",
                table: "PlayerOrMatchResult");

            migrationBuilder.DropIndex(
                name: "IX_PlayerOrMatchResult_OriginalMatchId",
                table: "PlayerOrMatchResult");
        }
    }
}

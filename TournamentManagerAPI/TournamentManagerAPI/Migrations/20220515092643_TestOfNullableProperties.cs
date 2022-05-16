using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    public partial class TestOfNullableProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Player_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayer_Player_PlayersId",
                table: "MatchPlayer");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.RenameColumn(
                name: "ShareLInk",
                table: "Tournaments",
                newName: "ShareLink");

            migrationBuilder.AlterColumn<string>(
                name: "ShareLink",
                table: "Tournaments",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tournaments",
                type: "TEXT",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 1000);

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TournamentId",
                table: "Players",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayer_Players_PlayersId",
                table: "MatchPlayer",
                column: "PlayersId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinnerId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchPlayer_Players_PlayersId",
                table: "MatchPlayer");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.RenameColumn(
                name: "ShareLink",
                table: "Tournaments",
                newName: "ShareLInk");

            migrationBuilder.AlterColumn<string>(
                name: "ShareLInk",
                table: "Tournaments",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Tournaments",
                type: "TEXT",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TournamentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Player_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Player_TournamentId",
                table: "Player",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Player_WinnerId",
                table: "Matches",
                column: "WinnerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchPlayer_Player_PlayersId",
                table: "MatchPlayer",
                column: "PlayersId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentManagerAPI.Migrations
{
    public partial class addedNameToMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Matches",
                type: "TEXT",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Matches");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI
{
    public sealed class AppDBContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
            dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");
    }
}

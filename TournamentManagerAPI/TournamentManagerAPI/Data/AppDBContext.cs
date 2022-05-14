using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Tournament>? Tournaments { get; set; }
        public DbSet<Match>? Matches { get; set; }
        public DbSet<Player>? Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
            dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");
    }
}

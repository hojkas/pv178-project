using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI
{
    public sealed class AppDBContext : DbContext
    {
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<PlayerOrMatchResult> PlayerOrMatchResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) =>
            dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasOne(m => m.PlayerRequiringResult)
                .WithOne(p => p.Match)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Match>()
                .HasMany(m => m.Players)
                .WithOne(m => m.OriginalMatch)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Tournament>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Tournament)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Player>()
                .HasMany<PlayerOrMatchResult>()
                .WithOne(p => p.Player)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<PlayerOrMatchResult>()
                .HasOne(p => p.Player)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<PlayerOrMatchResult>()
                .HasOne(p => p.Match)
                .WithOne(m => m.PlayerRequiringResult)
                .OnDelete(DeleteBehavior.ClientNoAction);

            modelBuilder.Entity<PlayerOrMatchResult>()
                .HasOne(p => p.OriginalMatch)
                .WithMany(m => m.Players)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}

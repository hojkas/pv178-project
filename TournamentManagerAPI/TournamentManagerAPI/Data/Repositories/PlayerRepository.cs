using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI.Data.Repositories
{
    internal static class PlayerRepository
    {
        internal static async Task<List<Player>> GetPlayersAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Players.ToListAsync();
            }
        }

        internal static async Task<List<Player>> GetTournamentPlayersAsync(Tournament tournament)
        {
            return await GetTournamentPlayersAsync(tournament.Id);
        }

        internal static async Task<List<Player>> GetTournamentPlayersAsync(int tournamentId)
        {
            return (await GetPlayersAsync() ?? new List<Player>())
                .Where(p => p.Tournament?.Id == tournamentId)
                .ToList();
        }
    }
}

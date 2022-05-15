using Microsoft.EntityFrameworkCore;
using TournamentManagerAPI.Data.Entities;

namespace TournamentManagerAPI.Data.Repositories
{
    internal static class PlayerRepository
    {
        internal static List<Player> GetPlayersAsync()
        {
            using (var db = new AppDBContext())
            {
                return db.Players.ToList();
            }
        }
    }
}

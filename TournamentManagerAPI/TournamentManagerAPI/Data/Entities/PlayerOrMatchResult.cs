using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class PlayerOrMatchResult
    {
        public int Id { get; set; }

        public bool IsPlayer { get; set; } = true;

        public int? PlayerId { get; set; }
        public Player? Player { get; set; }

        public int? MatchId { get; set; }
        public Match? Match { get; set; }

        public int OriginalMatchId { get; set; }

        [JsonIgnore]
        [InverseProperty("Players")]
        public Match? OriginalMatch { get; set; }

        public void UpdateAfterResolvedMatch()
        {
            if (IsPlayer || Match == null)
                throw new InvalidOperationException("Cannot update when match isn't configured");
            if (!Match.IsFinished || Match.Winner == null || Match.WinnerId == null)
                throw new InvalidOperationException("Cannot update when match isn't resolved");

            IsPlayer = true;
            PlayerId = Match.WinnerId;
            Player = Match.Winner;
        }
    }
}

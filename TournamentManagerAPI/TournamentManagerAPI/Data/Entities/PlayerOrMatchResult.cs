using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class PlayerOrMatchResult
    {
        public int Id { get; set; }

        public bool IsEmpty { get; set; }

        public bool IsPlayer { get; set; } = true;

        public int? PlayerId { get; set; }
        public Player? Player { get; set; }

        public int? MatchId { get; set; }
        [InverseProperty("MatchRequiringResult")]
        public Match? Match { get; set; }

        public int OriginalMatchId { get; set; }

        [JsonIgnore]
        [InverseProperty("Players")]
        public Match? OriginalMatch { get; set; }
    }
}

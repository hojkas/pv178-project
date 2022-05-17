using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Start { get; set; }

        [MaxLength(1000)]
        public string? Note { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Score { get; set; } = string.Empty;
        public bool IsFinished { get; set; } = false;
        
        [Required]
        public int TournamentId { get; set; }

        public List<PlayerOrMatchResult> Players { get; set; } = new();

        public int? WinnerId { get; set; }
        public Player? Winner { get; set; }

        public bool PlayerIsInMatch(int id)
        {
            foreach(var player in Players)
            {
                if (player.IsPlayer && player.Player != null && player.Player.Id == id)
                    return true;
            }
            return false;
        }
    }
}

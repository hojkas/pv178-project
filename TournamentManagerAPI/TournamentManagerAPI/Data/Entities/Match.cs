using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class Match
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Start { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Note { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Score { get; set; } = string.Empty;
        public bool IsFinished { get; set; } = false;
        
        [Required]
        public int TournamentId { get; set; }

        [InverseProperty("OriginalMatch")]
        public List<PlayerOrMatchResult> Players { get; set; } = new();

        public int? WinnerId { get; set; }
        public Player? Winner { get; set; }

        [ForeignKey("MatchRequiringResult")]
        public int? MatchRequiringResultId { get; set; }
        [JsonIgnore]
        [InverseProperty("Match")]
        public PlayerOrMatchResult? MatchRequiringResult { get; set; }
    }
}

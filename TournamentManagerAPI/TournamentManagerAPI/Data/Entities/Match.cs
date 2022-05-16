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
        [JsonIgnore]
        public Tournament? Tournament { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();

        public int? WinnerId { get; set; }
        public Player? Winner { get; set; }
    }
}

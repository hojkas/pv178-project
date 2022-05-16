using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsPublic { get; set; } = false;
        public string? ShareLink { get; set; }

        [Required]
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        [JsonIgnore]
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace TournamentManagerAPI.Data.Entities
{
    internal sealed class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsPublic { get; set; } = false;
        public string ShareLInk { get; set; } = string.Empty;

        [Required]
        public User? User { get; set; }

        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}

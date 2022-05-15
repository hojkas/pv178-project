using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Note { get; set; }

        [Required]
        public int TournamentId { get; set; }
        public Tournament? Tournament { get; set; }

        [InverseProperty("Players")]
        public ICollection<Match> Matches { get; set; } = new List<Match>();
        [InverseProperty("Winner")]
        public ICollection<Match> MatchesWon { get; set; } = new List<Match>();
    }
}

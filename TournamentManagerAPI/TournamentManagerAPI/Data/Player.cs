using System.ComponentModel.DataAnnotations;

namespace TournamentManagerAPI.Data
{
    internal sealed class Player
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Nickname { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string Note { get; set; } = string.Empty;

        public Tournament? Tournament { get; set; }
        public IList<Match> Matches { get; set; } = new List<Match>();
        public IList<Match> MatchesWon { get; set; } = new List<Match>();
    }
}

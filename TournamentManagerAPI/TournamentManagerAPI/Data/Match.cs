using System.ComponentModel.DataAnnotations;

namespace TournamentManagerAPI.Data
{
    internal sealed class Match
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [MaxLength(1000)]
        public string Note { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Score { get; set; } = string.Empty;
        public bool IsFinished { get; set; } = false;
        
        public Tournament? Tournament { get; set; }
        public Player[] Players { get; set; } = new Player[2];
        public Player? Winner { get; set; }
    }
}

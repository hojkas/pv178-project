using System.ComponentModel.DataAnnotations;

namespace TournamentManagerAPI.Data.Entities
{
    public sealed class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        // TODO if there is more time, move this to more secure location
        internal byte[]? PasswordHash { get; set; }
        internal byte[]? PasswordSalt { get; set; }

        public DateTime DateJoined { get; set; }

        public ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}

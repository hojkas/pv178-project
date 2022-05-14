using System.ComponentModel.DataAnnotations;

namespace TournamentManagerAPI.Data
{
    internal sealed class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty;

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime DateJoined { get; set; }
    }
}

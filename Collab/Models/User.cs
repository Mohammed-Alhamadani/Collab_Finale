using System.ComponentModel.DataAnnotations;

namespace Collab.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Username must be between 3 and 50 characters.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string PasswordHash { get; set; }

        public string? ProfilePictureURL { get; set; }

        public DateTime DateJoined { get; set; }

        // New property to store the user role
        public string Role { get; set; } = "User"; // Default role
    }
}
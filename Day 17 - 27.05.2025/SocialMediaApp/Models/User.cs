using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required,Phone]
        public string? PhoneNumber { get; set; } = string.Empty;

        public string? Status { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();

    }
}

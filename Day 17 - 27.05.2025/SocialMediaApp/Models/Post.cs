using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SocialMediaApp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;
        public DateTime PostedAt { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();

    }
}

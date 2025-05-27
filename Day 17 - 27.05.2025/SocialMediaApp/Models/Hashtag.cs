using System.ComponentModel.DataAnnotations;

namespace SocialMediaApp.Models
{
    public class Hashtag
    {
        [Key]
        public int Id { get; set; }

        public string Tag { get; set; }  = string.Empty;

        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
    }
}

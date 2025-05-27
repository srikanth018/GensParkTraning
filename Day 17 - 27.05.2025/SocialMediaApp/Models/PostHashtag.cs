using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class PostHashtag
    {
        public int Id { get; set; }
        public int PostId { get; set; }

        public int HashtagId { get; set; }

        [ForeignKey("PostId")]
        public Post? Post { get; set; } 

        [ForeignKey("HashtagId")]
        public Hashtag? Hashtag { get; set; } 
    }
}

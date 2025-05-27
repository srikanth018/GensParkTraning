using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.Models
{
    public class UserFollow
    {
        [Key]
        public int SerialNumber { get; set; }
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }

        [ForeignKey("FollowerId")]
        public User? Follower { get; set; }

        [ForeignKey("FolloweeId")]
        public User? Followee { get; set; }
    }
}

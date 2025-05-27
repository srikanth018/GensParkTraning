
using Microsoft.EntityFrameworkCore;
using SocialMediaApp.Models;

namespace SocialMediaApp.Contexts
{
    public class SocialMediaContext : DbContext
    {
        public SocialMediaContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<PostHashtag> PostHashtags { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
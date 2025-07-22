using Microsoft.EntityFrameworkCore;
using VideoStreamApp.Models;

namespace VideoStreamApp.Contexts
{
    public class VideoDbContext : DbContext
    {
        public VideoDbContext(DbContextOptions<VideoDbContext> options)
            : base(options)
        {
        }

        public DbSet<VideoData> Videos { get; set; }
    }
}
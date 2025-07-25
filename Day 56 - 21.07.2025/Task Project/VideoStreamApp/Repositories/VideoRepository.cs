using Microsoft.EntityFrameworkCore;
using VideoStreamApp.Contexts;
using VideoStreamApp.Interfaces;
using VideoStreamApp.Models;

namespace VideoStreamApp.Repositories
{
    public class VideoRepository : IRepository<VideoData>
    {
        private readonly VideoDbContext _context;

        public VideoRepository(VideoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VideoData>> GetAllAsync()
        {
            return await _context.Videos.ToListAsync();
        }

        public async Task<VideoData?> GetByIdAsync(Guid id)
        {
            return await _context.Videos.FindAsync(id);
        }

        public async Task AddAsync(VideoData entity)
        {
            await _context.Videos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}

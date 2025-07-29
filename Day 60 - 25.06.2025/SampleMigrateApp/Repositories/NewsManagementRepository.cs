using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;

namespace TrueVote.Repositories
{
    public class NewsManagementRepository : Repository<int, News>
    {
        public NewsManagementRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}

        public override async Task<News?> Get(int key)
        {
            return await _appDbContext.News
                .SingleOrDefaultAsync(c => c.NewsId == key);
        }

        public override async Task<IEnumerable<News>> GetAll()
        {
            return await _appDbContext.News
                .ToListAsync();
        }
    }
}

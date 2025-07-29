using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;

namespace TrueVote.Repositories
{
    public class ColorsRepository : Repository<int, Color>
    {
        public ColorsRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}

        public override async Task<Color?> Get(int key)
        {
            return await _appDbContext.Colors
                .Include(c => c.Products)
                .SingleOrDefaultAsync(c => c.ColorId == key);
        }

        public override async Task<IEnumerable<Color>> GetAll()
        {
            return await _appDbContext.Colors
                .Include(c => c.Products)
                .ToListAsync();
        }
    }
}

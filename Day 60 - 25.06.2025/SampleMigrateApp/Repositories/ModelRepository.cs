using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;

namespace SampleMigrateApp.Repositories
{
    public class ModelRepository : Repository<int, Model>
    {
        public ModelRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}

        public override async Task<Model?> Get(int key)
        {
            return await _appDbContext.Models
                .Include(c => c.Products)
                .SingleOrDefaultAsync(c => c.ModelId == key);
        }

        public override async Task<IEnumerable<Model>> GetAll()
        {
            return await _appDbContext.Models
                .Include(c => c.Products)
                .ToListAsync();
        }
    }
}

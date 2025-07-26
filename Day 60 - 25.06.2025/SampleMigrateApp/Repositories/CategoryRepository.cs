using System.Data.Entity;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Repositories
{
    public class CategoryRepository : Repository<int, Category>
    {
        public CategoryRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}
        public override async Task<Category?> Get(int key)
        {
            return await _appDbContext.Categories.SingleOrDefaultAsync(c => c.CategoryId == key);
        }

        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _appDbContext.Categories.ToListAsync();
        }
    }
}
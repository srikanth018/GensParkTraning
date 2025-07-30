using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;


namespace SampleMigrateApp.Repositories
{
    public class ProductRepository : Repository<int, Product>
    {
        public ProductRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}
        public override async Task<Product?> Get(int key)
        {
            return await _appDbContext.Products
                    .SingleOrDefaultAsync(p => p.ProductId == key);
        }

        public override async Task<IEnumerable<Product>> GetAll()
        {
            return await _appDbContext.Products
                            .ToListAsync();
        }
    }
}
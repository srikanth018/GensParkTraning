using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Repositories
{
    public class CartRepository : Repository<int, Cart>
    {
        public CartRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}
        public override async Task<Cart?> Get(int key)
        {
            return await _appDbContext.Carts.SingleOrDefaultAsync(c => c.CartId == key);
        }

        public override async Task<IEnumerable<Cart>> GetAll()
        {
            return await _appDbContext.Carts.ToListAsync();
        }
    }
}
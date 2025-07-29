using Microsoft.EntityFrameworkCore;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;

namespace TrueVote.Repositories
{
    public class OrderRepository : Repository<int, Order>
    {
        public OrderRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}

        public override async Task<Order?> Get(int key)
        {
            return await _appDbContext.Orders
                .SingleOrDefaultAsync(c => c.OrderID == key);
        }

        public override async Task<IEnumerable<Order>> GetAll()
        {
            return await _appDbContext.Orders
                .ToListAsync();
        }
    }
}

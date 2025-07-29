using System.Data.Entity;
using SampleMigrateApp.Contexts;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Repositories
{
    public class OrderDetailRepository : Repository<int, OrderDetail>
    {
        public OrderDetailRepository(ChienVHShopDBEntities appDbContext) : base(appDbContext)
        {}
        public override async Task<OrderDetail?> Get(int key)
        {
            return await _appDbContext.OrderDetails.SingleOrDefaultAsync(c => c.OrderID == key);
        }

        public override async Task<IEnumerable<OrderDetail>> GetAll()
        {
            return await _appDbContext.OrderDetails.ToListAsync();
        }
    }
}
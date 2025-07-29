using SampleMigrateApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMigrateApp.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetPagedOrdersAsync(int pageNumber, int pageSize);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}

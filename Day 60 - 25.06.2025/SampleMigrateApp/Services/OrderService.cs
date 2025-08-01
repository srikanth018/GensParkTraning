using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;
using SampleMigrateApp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMigrateApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Order> _orderRepository;

        public OrderService(IRepository<int, Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> GetPagedOrdersAsync(int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetAll();
            return orders
                .OrderByDescending(o => o.OrderID)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.Get(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _orderRepository.Add(order);
            return order;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existing = await _orderRepository.Get(order.OrderID);
            if (existing == null)
                return false;

            await _orderRepository.Update(existing.OrderID, order);
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.Get(id);
            if (order == null)
                return false;

            await _orderRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var allOrders = await _orderRepository.GetAll();

            // Filter orders that have at least one OrderDetail with the given userId
            var userOrders = allOrders
                .Where(o => o.OrderDetails != null && o.OrderDetails.Any(od => od.UserId == userId))
                .Select(o => new Order
                {
                    OrderID = o.OrderID,
                    OrderName = o.OrderName,
                    OrderDate = o.OrderDate,
                    PaymentType = o.PaymentType,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    CustomerName = o.CustomerName,
                    CustomerPhone = o.CustomerPhone,
                    CustomerEmail = o.CustomerEmail,
                    CustomerAddress = o.CustomerAddress,
                    OrderDetails = o.OrderDetails
                        .Where(od => od.UserId == userId)
                        .Select(od => new OrderDetail
                        {
                            OrderID = od.OrderID,
                            ProductId = od.ProductId,
                            Price = od.Price,
                            Quantity = od.Quantity,
                            UserId = od.UserId,
                            Product = od.Product,
                            User = null // Avoid circular reference
                        })
                        .ToList()
                })
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return userOrders;
        }


    }
}

using System.Threading.Tasks;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<int, Cart> _cartRepo;
        private readonly IRepository<int, Product> _productRepo;
        private readonly IRepository<int, Order> _orderRepo;
        private readonly IRepository<int, OrderDetail> _orderDetailRepo;

        public ShoppingCartService(IRepository<int, Cart> cartRepo, IRepository<int, Product> productRepo, IRepository<int, Order> orderRepo, IRepository<int, OrderDetail> orderDetailRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
        }
        public async Task<Cart> AddToCart(AddToCartDTO addToCartDTO)
        {
            var product = await GetProductById(addToCartDTO.ProductId);
            if (product.Price == null)
                throw new InvalidOperationException("Product price is not set.");
            var totalPrice = (double)product.Price * addToCartDTO.Quantity;

            var cartItem = new Cart
            {
                UserId = addToCartDTO.UserId,
                ProductId = addToCartDTO.ProductId,
                Quantity = addToCartDTO.Quantity,
                TotalPrice = totalPrice,
                CreatedDate = DateTime.UtcNow
            };

            var newCart = await _cartRepo.Add(cartItem);
            if (newCart == null)
                throw new ArgumentNullException("Item not added to the  cart");

            return newCart;
        }


        public async Task<Cart> UpdateProductQuantity(UpdateProductQuantityDTO quantityDTO)
        {
            var existingCartItems = await GetCartItemsByUserId(quantityDTO.UserId);
            var cartItem = existingCartItems.FirstOrDefault(c => c.ProductId == quantityDTO.ProductId);
            if (cartItem == null)
                throw new KeyNotFoundException("Cart Item not found for the provided user Id and product Id");
            if (cartItem.Quantity == quantityDTO.Quantity)
                throw new ArgumentException("Update not possible as the new quantity and existing quantity are same");
            if (quantityDTO.Quantity < 0)
                throw new ArgumentException($"You cannot Update the quantity by {quantityDTO.Quantity}");

            var product = await GetProductById(quantityDTO.ProductId);
            if (product.Price == null)
                throw new InvalidOperationException("Product price is not set.");
            var totalPrice = (double)product.Price * quantityDTO.Quantity;

            cartItem.Quantity = quantityDTO.Quantity;
            cartItem.TotalPrice = totalPrice;

            var updatedCartItem = await _cartRepo.Update(cartItem.CartId, cartItem);
            if (updatedCartItem == null)
                throw new Exception("Update Quantity Failed");
            return updatedCartItem;
        }

        public async Task<IEnumerable<Cart>> GetCartItemsByUserId(int id)
        {
            var existingCartItems = await _cartRepo.GetAll();
            existingCartItems = existingCartItems.Where(c => c.UserId == id);
            return existingCartItems;
        }

        public async Task<Cart> RemoveFromCart(int cartId)
        {
            var cartItem = await isItemExists(cartId);
            var removedItem = await _cartRepo.Delete(cartItem.CartId);
            return removedItem;
        }

        private async Task<Cart> isItemExists(int itemId)
        {
            if (itemId < 0)
                throw new ArgumentException("Item Id Cannot be less than zero");
            var item = await _cartRepo.Get(itemId);
            if (item == null)
                throw new KeyNotFoundException("Cart Item not exists");
            return item;
        }
        private async Task<Product> GetProductById(int id)
        {
            var product = await _productRepo.Get(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task<Order> PlaceOrder(PlaceOrderDTO placeOrderDTO)
        {
            var cartItems = await GetCartItemsByUserId(placeOrderDTO.UserId);
            var order = new Order
            {
                CustomerName = placeOrderDTO.CustomerName,
                CustomerEmail = placeOrderDTO.CustomerEmail,
                CustomerAddress = placeOrderDTO.CustomerAddress,
                CustomerPhone = placeOrderDTO.CustomerPhone,
                OrderDate = DateTime.UtcNow,
                PaymentType = "Cash",
                Status = "Processing"
            };
            await _orderRepo.Add(order);
            foreach (Cart cart in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    Price = cart.TotalPrice
                };
                await _orderDetailRepo.Add(orderDetail);
                await _cartRepo.Delete(cart.CartId);
            }
            return order;
        }
    }
}
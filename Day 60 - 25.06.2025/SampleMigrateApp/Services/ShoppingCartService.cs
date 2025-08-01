using System.Threading.Tasks;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Migrations;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<int, Cart> _cartRepo;
        private readonly IRepository<int, Product> _productRepo;
        private readonly IRepository<int, Order> _orderRepo;
        private readonly IRepository<int, OrderDetail> _orderDetailRepo;
        private readonly IProductService _productService;

        public ShoppingCartService(IRepository<int, Cart> cartRepo, IRepository<int, Product> productRepo, IRepository<int, Order> orderRepo, IRepository<int, OrderDetail> orderDetailRepo, IProductService productService)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _orderDetailRepo = orderDetailRepo;
            _productService = productService;
        }
        public async Task<Cart> AddToCart(AddToCartDTO addToCartDTO)
        {
            Console.WriteLine($"{addToCartDTO.ProductId} - {addToCartDTO.UserId} - {addToCartDTO.Quantity}");

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

            var cartItem = await isItemExists(quantityDTO.CartId);
            if (cartItem == null)
                throw new KeyNotFoundException("Cart Item not found for the provided Cart Id");
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

        private async Task<IEnumerable<Cart>> GetCartItems(int userId)
        {
            if (userId < 0)
                throw new ArgumentException("User Id Cannot be less than zero");
            var item = await _cartRepo.GetAll();
            if (item == null)
                throw new KeyNotFoundException("Cart Item not exists");
            return item.Where(c => c.UserId == userId);
        }


        public async Task<IEnumerable<CartResponseDTO>> GetCartItemsByUserId(int id)
        {
            var existingCartItems = await GetCartItems(id);
            if (existingCartItems == null || !existingCartItems.Any())
                throw new ArgumentException("No Cart Items Found for this User Id");
            if (existingCartItems.Count() == 0)
                return Enumerable.Empty<CartResponseDTO>();
            var cartResponseList = new List<CartResponseDTO>();
            foreach (var cart in existingCartItems)
            {
                var product = await GetFullProductById(cart.ProductId);
                if (product == null)
                    throw new KeyNotFoundException("Product not found for the Cart Item");
                var cartResponse = new CartResponseDTO
                {
                    CartId = cart.CartId,
                    UserId = cart.UserId,
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    TotalPrice = cart.TotalPrice,
                    CreatedDate = cart.CreatedDate,
                    Product = product
                };
                cartResponseList.Add(cartResponse);
            }
            return cartResponseList;
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

        private async Task<ProductResponseDTO> GetFullProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");
            return product;
        }

        public async Task<Order> PlaceOrder(PlaceOrderDTO placeOrderDTO)
        {
            var cartItems = await GetCartItems(placeOrderDTO.UserId);

            var order = new Order
            {
                CustomerName = placeOrderDTO.CustomerName,
                CustomerEmail = placeOrderDTO.CustomerEmail,
                CustomerAddress = placeOrderDTO.CustomerAddress,
                CustomerPhone = placeOrderDTO.CustomerPhone,
                OrderDate = DateTime.UtcNow,
                PaymentType = placeOrderDTO.PaymentType,
                TotalAmount = placeOrderDTO.TotalAmount,
                Status = "Processing",
                OrderDetails = new List<OrderDetail>()
            };

            foreach (Cart cart in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    Price = cart.TotalPrice,
                    UserId = placeOrderDTO.UserId
                };

                order.OrderDetails.Add(orderDetail);
                await _cartRepo.Delete(cart.CartId);
            }

            await _orderRepo.Add(order);

            return order;
        }
    }
}
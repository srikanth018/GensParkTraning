using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Interfaces
{
    public interface IShoppingCartService
    {
        Task<Cart> AddToCart(AddToCartDTO addToCartDTO);
        Task<Cart> UpdateProductQuantity(UpdateProductQuantityDTO quantityDTO);
        Task<Cart> RemoveFromCart(int cartId);
        Task<IEnumerable<Cart>> GetCartItemsByUserId(int id);
        Task<Order> PlaceOrder(PlaceOrderDTO placeOrderDTO);
    }
}
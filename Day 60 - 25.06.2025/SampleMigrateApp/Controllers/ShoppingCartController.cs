using Microsoft.AspNetCore.Mvc;
using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;
using SampleMigrateApp.Services;

namespace SampleMigrateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<Cart>> AddToCart([FromBody] AddToCartDTO dto)
        {
            try
            {
                System.Console.WriteLine($"Adding to cart: ProductId={dto.ProductId}, UserId={dto.UserId}, Quantity={dto.Quantity}");
                var result = await _shoppingCartService.AddToCart(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update-quantity")]
        public async Task<ActionResult<Cart>> UpdateQuantity([FromBody] UpdateProductQuantityDTO dto)
        {
            try
            {
                var result = await _shoppingCartService.UpdateProductQuantity(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartItemsByUser(int userId)
        {
            try
            {
                var items = await _shoppingCartService.GetCartItemsByUserId(userId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("remove/{cartId}")]
        public async Task<ActionResult<Cart>> RemoveFromCart(int cartId)
        {
            try
            {
                var removed = await _shoppingCartService.RemoveFromCart(cartId);
                return Ok(removed);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("place-order")]
        public async Task<ActionResult<Order>> PlaceOrder([FromBody] PlaceOrderDTO dto)
        {
            try
            {
                var order = await _shoppingCartService.PlaceOrder(dto);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}


namespace SampleMigrateApp.DTOs
{
    public class CartResponseDTO
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductResponseDTO Product { get; set; } = new ProductResponseDTO();
    }
}

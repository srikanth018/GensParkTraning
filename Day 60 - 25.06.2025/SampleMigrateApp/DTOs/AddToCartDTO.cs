namespace SampleMigrateApp.DTOs
{
    public class AddToCartDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
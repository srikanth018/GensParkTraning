namespace SampleMigrateApp.DTOs
{
    public class UpdateProductQuantityDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
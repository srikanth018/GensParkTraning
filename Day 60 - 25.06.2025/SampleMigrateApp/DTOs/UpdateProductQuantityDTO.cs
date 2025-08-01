namespace SampleMigrateApp.DTOs
{
    public class UpdateProductQuantityDTO
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
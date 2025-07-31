namespace SampleMigrateApp.DTOs
{
    public class ProductResponseDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public double? Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        public DateTime SellStartDate { get; set; }
        public DateTime SellEndDate { get; set; }
    }
}

namespace SampleMigrateApp.DTOs
{
    public class PlaceOrderDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public double TotalAmount { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
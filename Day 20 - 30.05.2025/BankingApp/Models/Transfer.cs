namespace BankApp.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string SenderAccNum { get; set; } = string.Empty;
        public string RecevierAccNum { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public Transaction? Transaction { get; set; }
        public int AccountHolderId { get; set; }
        public AccountHolder? AccountHolder { get; set; }
        public string? Description { get; set; } = string.Empty;
        public string? Status { get; set; } = "Pending";
        
    }
}
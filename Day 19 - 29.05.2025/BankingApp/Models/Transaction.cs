namespace BankApp.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int AccountHolderId { get; set; }
        public DateTime TransactionDate { get; set; }
        public AccountHolder? AccountHolder { get; set; }
        public Transfer? Transfer { get; set; }
    }
}
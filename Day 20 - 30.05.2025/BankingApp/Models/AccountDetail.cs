namespace BankApp.Models
{
    public class AccountDetail
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; } = 0.0m;
        public AccountHolder? AccountHolder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    
}
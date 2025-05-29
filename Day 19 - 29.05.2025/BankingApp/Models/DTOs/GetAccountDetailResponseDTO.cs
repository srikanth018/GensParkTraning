namespace BankApp.Models.DTOs
{ 
    public class GetAccountDetailResponseDTO
    {
        public string? AccountNumber { get; set; } 
        public string? AccountType { get; set; } 
        public decimal Balance { get; set; }
    }
}
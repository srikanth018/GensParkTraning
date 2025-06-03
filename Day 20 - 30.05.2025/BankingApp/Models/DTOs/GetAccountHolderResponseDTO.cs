namespace BankApp.Models.DTOs
{ 
    public class GetAccountHolderResponseDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public GetAddressResponseDTO? Address { get; set; }
        public GetAccountDetailResponseDTO? AccountDetail { get; set; }
    }
}
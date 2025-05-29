namespace BankApp.Models
{
    public class AccountHolder
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 

        public int AddressId { get; set; }
        public Address? Address { get; set; }

        public int AccountDetailId { get; set; }
        public AccountDetail? AccountDetail { get; set; }

        public string Status { get; set; } = "Active";
    }
}

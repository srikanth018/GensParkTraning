namespace BankApp.Models.DTOs
{
    public class AddAccountHolderRequestDTO
    {
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public AddAddressRequestDTO? Address { get; set; }
    public AddAccountDetailRequestDTO? AccountDetail { get; set; }
    }
    
}
using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Misc
{
    public static class AccountHolderMapper
    {
        public static AccountHolder MapToAccountHolder(AddAccountHolderRequestDTO request)
        {
            return new AccountHolder
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth
            };
        }
        public static Address MapToAddress(AddAddressRequestDTO request)
        {
            return new Address
            {
                Street = request.Street,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode
            };
        }
        public static AccountDetail MapToAccountDetail(AddAccountDetailRequestDTO request)
        {
            return new AccountDetail
            {
                AccountNumber = Generator.GenerateAccountNumber(),
                Balance = 0.0m,
                AccountType = request.AccountType
            };
        }
    }
}
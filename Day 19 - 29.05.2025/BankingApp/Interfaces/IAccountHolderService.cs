using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{ 
    public interface IAccountHolderService
    {
        Task<IEnumerable<AccountHolder>> GetAllAccountHoldersAsync();
        Task<AccountHolder?> GetAccountHolderByIdAsync(int id);
        Task<AccountHolder> AddAccountHolderAsync(AddAccountHolderRequestDTO accountHolder);
    }
}
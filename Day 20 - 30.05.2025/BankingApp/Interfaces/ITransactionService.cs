using BankApp.Models;
using BankApp.Models.DTOs;

namespace BankApp.Interfaces
{ 
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        // Task<Transaction> AddTransactionAsync(AddTransactionRequestDTO transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task<IEnumerable<Transaction>> GetTransactionsByAccountHolderIdAsync(int accountHolderId);
    }
}
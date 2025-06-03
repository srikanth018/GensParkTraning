using BankApp.Contexts;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories
{ 
    public class TransactionRepository : Repository<string, Transaction>
    {
        public TransactionRepository(BankingContext bankingContext) : base(bankingContext)
        {
        }

        public override async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _bankingContext.Transactions
                .Include(t => t.AccountHolder)
                .Include(t => t.Transfer)
                .ToListAsync();
        }

        public override Task<Transaction?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
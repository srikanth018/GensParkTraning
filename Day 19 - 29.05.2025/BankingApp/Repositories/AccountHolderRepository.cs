
using System.Collections.Generic;
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories
{
    public class AccountHolderRepository : Repository<int, AccountHolder>
    {
        public AccountHolderRepository(BankingContext bankingContext) : base(bankingContext)
        {
        }

        public override async Task<IEnumerable<AccountHolder>> GetAllAsync()
        {
            return await _bankingContext.AccountHolders.ToListAsync();
        }

        public override async Task<AccountHolder?> GetByIdAsync(int id)
        {
            return await _bankingContext.AccountHolders
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
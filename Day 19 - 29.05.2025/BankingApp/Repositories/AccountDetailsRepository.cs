
using System.Collections.Generic;
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories
{ 
    public class AccountDetailsRepository : Repository<int, AccountDetail>
    {
        public AccountDetailsRepository(BankingContext bankingContext) : base(bankingContext)
        {
        }

        public override async Task<IEnumerable<AccountDetail>> GetAllAsync()
        {
            return await _bankingContext.AccountDetails.ToListAsync();
        }

        public override async Task<AccountDetail?> GetByIdAsync(int id)
        {
            return await _bankingContext.AccountDetails
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
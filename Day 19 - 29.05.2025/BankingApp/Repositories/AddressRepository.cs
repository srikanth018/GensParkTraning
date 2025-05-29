
using System.Collections.Generic;
using BankApp.Contexts;
using BankApp.Interfaces;
using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Repositories
{

    public class AddressRepository : Repository<int, Address>
    {
        public AddressRepository(BankingContext bankingContext) : base(bankingContext)
        {
        }

        public override async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _bankingContext.Addresses.ToListAsync();
        }

        public override async Task<Address?> GetByIdAsync(int id)
        {
            return await _bankingContext.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
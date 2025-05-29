using BankApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Contexts
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options)
        {
        }

        public DbSet<AccountDetail> AccountDetails { get; set; }
        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>()
                .HasOne(ah => ah.Address)
                .WithOne(a => a.AccountHolder)
                .HasForeignKey<AccountHolder>(ah => ah.AddressId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccountHolder>()
                .HasOne(ah => ah.AccountDetail)
                .WithOne(ad => ad.AccountHolder)
                .HasForeignKey<AccountHolder>(ah => ah.AccountDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }

}
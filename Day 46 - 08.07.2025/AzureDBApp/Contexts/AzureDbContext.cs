using AzureDBApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AzureDBApp.Contexts
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
    }
}
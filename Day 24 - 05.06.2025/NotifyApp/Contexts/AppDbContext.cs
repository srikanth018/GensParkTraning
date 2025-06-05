using Microsoft.EntityFrameworkCore;
using NotifyApp.Models;

namespace NotifyApp.Contexts
{
    public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();
}
}
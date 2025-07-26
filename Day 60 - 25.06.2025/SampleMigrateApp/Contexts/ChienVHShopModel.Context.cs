
namespace SampleMigrateApp.Contexts
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using SampleMigrateApp.Models;

    public partial class ChienVHShopDBEntities : DbContext
    {
        public ChienVHShopDBEntities(DbContextOptions<ChienVHShopDBEntities> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
    }
}

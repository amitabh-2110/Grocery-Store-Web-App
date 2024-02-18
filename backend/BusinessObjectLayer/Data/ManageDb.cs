using BusinessObjectLayer.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Data
{
    public class ManageDb: DbContext
    {
        public ManageDb(DbContextOptions options): base(options) 
        { 
        
        }

        public DbSet<RegisteredPerson> RegisteredPersons { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasKey(p => p.OrderId);
            modelBuilder.Entity<Orders>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .HasKey(p => new { p.Email, p.ProductId});
            modelBuilder.Entity<Cart>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<RegisteredPerson>()
                .HasKey(p => p.Email);

            modelBuilder.Entity<Products>()
                .HasKey(p => p.ProductId);
            modelBuilder.Entity<Products>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}

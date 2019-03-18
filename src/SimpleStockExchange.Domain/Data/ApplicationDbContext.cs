using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStockExchange.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBook { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            modelBuilder.Entity<OrderBook>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Order>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(b => b.Orders);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.OrderBook)
                .WithMany(b => b.Orders);
            

            modelBuilder.Seed();
        }
    }

}

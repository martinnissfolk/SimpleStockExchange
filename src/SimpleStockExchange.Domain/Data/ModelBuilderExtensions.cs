using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;

namespace SimpleStockExchange.Domain.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var id1 = Guid.NewGuid().ToString();

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                { 
                    Id = Guid.NewGuid().ToString(),
                    UserName = "bill",
                    NormalizedUserName = "BILL",
                    Email = "null",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEHRgQZRQBmPT/fQ6GTWTmGAD1BgV39Go5K30FQCF2rpMBuZbaDqVGCiydU3SDLHDIQ==",
                    SecurityStamp = "PJPGIXFAASFEALFPQB4ME4IJJUFNLBZW",
                    ConcurrencyStamp = "a288299f-a3bc-47e4-a158-a713928e91b2",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                },
                new ApplicationUser
                {
                    Id = id1,
                    UserName = "joe",
                    NormalizedUserName = "JOE",
                    Email = null,
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEEBAo3F2z0LOFuujNFX0ycGoF6wXrr1X0wcWGhFQjeio8p13zksBB9k7dH15QwvPNw==",
                    SecurityStamp = "YB27YKWP4TUJIFZ6BJZCOD3NTRDT2KKY",
                    ConcurrencyStamp = "c9b71971-a3d3-4213-85e5-4e0fa4caf9a3",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                }
            );

            modelBuilder.Entity<OrderBook>().HasData(
                new OrderBook {Id = 1, CompanyName = "Stock A", CurrentPrice = 10, Quantity = 100 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, UserId = id1, OrderStatus = 0, Date = DateTime.Now.AddDays(-2), Quantity = 5, OrderType = true, Price = 8, OrderBookId = 1},
                new Order { Id = 2, UserId = id1, OrderStatus = 0, Date = DateTime.Now.AddDays(-1), Quantity = 8, OrderType = true, Price = 10, OrderBookId = 1 },
                new Order { Id = 3, UserId = id1, OrderStatus = 0, Date = DateTime.Now.AddDays(-3), Quantity = 10, OrderType = false, Price = 12, OrderBookId = 1 },
                new Order { Id = 4, UserId = id1, OrderStatus = 0, Date = DateTime.Now.AddDays(-3), Quantity = 20, OrderType = false, Price = 15, OrderBookId = 1 }
            );

            
        }
    }
}

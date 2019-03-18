using SimpleStockExchange.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Clauses;

namespace SimpleStockExchange.Services
{
    public class OrderService : IOrderService
    {
        private ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(string userId, int price, int amount, bool orderType)
        {
            var currentPrice = 0;
            var orderWasMatched = true;
            if (orderType == true)
            {
                var activeAskOrders = await _context.Orders
                    .Where(x => 
                        x.OrderStatus == 0 && 
                        x.Price == price && 
                        x.OrderType == false)
                    .OrderByDescending(u => u.Date).ToListAsync();

                if (activeAskOrders.Count == 0)
                {
                    orderWasMatched = false;
                }

                foreach (var askOrder in activeAskOrders)
                {
                    if (amount == 0)
                    {
                        continue;
                    }

                    if (askOrder.Quantity > amount) //ask order needs to split into two parts
                    {
                        var remaining = askOrder.Quantity - amount;
                        askOrder.OrderStatus = 1;
                        askOrder.Quantity = amount;
                        _context.Orders.Add(new Order()
                        {
                            Price = askOrder.Price,
                            Quantity = remaining,
                            Date = askOrder.Date,
                            UserId = askOrder.UserId,
                            OrderBookId = askOrder.OrderBookId,
                            OrderType = askOrder.OrderType,
                            OrderStatus = 0
                        });

                        _context.Orders.Add(new Order()
                        {
                            Date = DateTime.Now,
                            UserId = userId,
                            OrderBookId = 1,
                            Quantity = amount,
                            Price = price,
                            OrderType = true,
                            OrderStatus = 1
                        });
                        orderWasMatched = false;
                    }
                    else if(askOrder.Quantity < amount)
                    {
                        amount = amount - askOrder.Quantity;
                        askOrder.OrderStatus = 1;

                        _context.Orders.Add(new Order()
                        {
                            Date = DateTime.Now,
                            UserId = userId,
                            OrderBookId = 1,
                            Quantity = askOrder.Quantity,
                            Price = price,
                            OrderType = true,
                            OrderStatus = orderWasMatched ? 1 : 0
                        });

                    }
                    else
                    {
                        askOrder.OrderStatus = 1;
                        orderWasMatched = true;
                    }

                    currentPrice = askOrder.Price;
                }
            }
            else
            {
                var activeBidOrders = await _context.Orders.Where(x => x.OrderStatus == 0
                                                                 && x.Price >= price
                                                                 && x.OrderType == true).OrderByDescending(u => u.Date).ToListAsync();
                if (activeBidOrders.Count == 0)
                {
                    orderWasMatched = false;
                }

                foreach (var bidOrder in activeBidOrders)
                {
                    if (amount == 0)
                    {
                        continue;
                    }

                    if (bidOrder.Quantity > amount)
                    {
                        var remaining = bidOrder.Quantity - amount;
                        bidOrder.OrderStatus = 1;
                        bidOrder.Quantity = amount;
                        _context.Orders.Add(new Order()
                        {
                            Price = bidOrder.Price,
                            Quantity = remaining,
                            Date = bidOrder.Date,
                            UserId = bidOrder.UserId,
                            OrderBookId = bidOrder.OrderBookId,
                            OrderType = bidOrder.OrderType,
                            OrderStatus = 0
                        });

                        _context.Orders.Add(new Order()
                        {
                            Date = DateTime.Now,
                            UserId = userId,
                            OrderBookId = 1,
                            Quantity = amount,
                            Price = price,
                            OrderType = false,
                            OrderStatus = 1
                        });
                        orderWasMatched = false;
                    }
                    else if (bidOrder.Quantity < amount)
                    {
                        amount = amount - bidOrder.Quantity;
                        bidOrder.OrderStatus = 1;

                        _context.Orders.Add(new Order()
                        {
                            Date = DateTime.Now,
                            UserId = userId,
                            OrderBookId = 1,
                            Quantity = bidOrder.Quantity,
                            Price = price,
                            OrderType = false,
                            OrderStatus = orderWasMatched ? 1 : 0
                        });

                    }
                    else
                    {
                        amount = amount - bidOrder.Quantity;
                        bidOrder.OrderStatus = 1;
                        orderWasMatched = true;
                    }

                    currentPrice = bidOrder.Price;
                }
            }

            var orderBook = await _context.OrderBook.FirstOrDefaultAsync();
            orderBook.CurrentPrice = currentPrice != 0 ? currentPrice : orderBook.CurrentPrice;

            if (amount != 0)
            {
                _context.Orders.Add(new Order()
                {
                    Date = DateTime.Now,
                    UserId = userId,
                    OrderBookId = 1,
                    Quantity = amount,
                    Price = price,
                    OrderType = orderType,
                    OrderStatus = orderWasMatched ? 1 : 0
                });
            }

            await _context.SaveChangesAsync();
        }

        public Task<OrderBook> GetOrderBookAsync()
        {
            return _context.Orders
                .Include(x => x.OrderBook)
                .Where(y => y.OrderStatus == 0)
                .Select(y => y.OrderBook)
                .Include(u => u.Orders)
                .FirstOrDefaultAsync();

            //return _context.Orders
            //    .Include(x => x.OrderBook)
            //    .Where(y => y.OrderStatus == 0)
            //    .Select(y => y.OrderBook)
            //    .FirstOrDefaultAsync();
        }

    }
}

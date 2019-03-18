using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleStockExchange.Domain.Data;

namespace SimpleStockExchange.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(string userId, int price, int amount, bool orderType);
        Task<OrderBook> GetOrderBookAsync();
    }
}

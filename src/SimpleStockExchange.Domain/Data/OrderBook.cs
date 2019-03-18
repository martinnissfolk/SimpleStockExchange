using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStockExchange.Domain.Data
{
    public class OrderBook
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TickerSymbol { get; set; }
        public int CurrentPrice { get; set; }


        //public int Id { get; set; }
        public ICollection<Order> Orders { get; set; }
        public int Quantity { get; set; }
        //public Stock Stock { get; set; }
    }
}

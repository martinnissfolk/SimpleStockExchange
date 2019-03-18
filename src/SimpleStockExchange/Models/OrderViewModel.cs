using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStockExchange.Models
{
    public class OrderViewModel
    {
        public OrderBookViewModel OrderBookDetails { get; set; }
        public BuyViewModel Buy { get; set; }
        public SellViewModel Sell { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStockExchange.Models
{
    public class OrderBookViewModel
    {
        public string Name { get; set; }
        public int CurrentPrice { get; set; }
        public List<PairViewModel> Bids { get; set; }
        public List<PairViewModel> Asks { get; set; }
    }

    public class PairViewModel
    {
        public int Amount { get; set; }
        public int Price { get; set; }
        public DateTime Time { get; set; }
    }

}

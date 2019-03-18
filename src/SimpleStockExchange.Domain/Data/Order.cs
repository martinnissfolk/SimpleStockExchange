using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStockExchange.Domain.Data
{
    public class Order
    {
        /// <summary>
        /// Gets/sets the unique id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets/sets the user of the order.
        /// </summary>
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }

        public int OrderStatus { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public bool OrderType { get; set; }

        public OrderBook OrderBook { get; set; }

        public int OrderBookId { get; set; }
    }
}

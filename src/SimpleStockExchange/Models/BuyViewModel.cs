using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleStockExchange.Models
{
    public class BuyViewModel
    {
        [Required]
        public int Amount { get; set; }

        [Required]
        public int Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SimpleStockExchange.Domain.Data
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets/sets the users orders.
        /// </summary>
        public ICollection<Order> Orders { get; set; }
    }
}

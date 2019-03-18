using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleStockExchange.Domain.Data;
using SimpleStockExchange.Models;
using SimpleStockExchange.Services;

namespace SimpleStockExchange.Controllers
{
    public class HomeController : Controller
    {
        private IOrderService _orderService;

        public HomeController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await GetOrderBookAsync();
            return View(model);
        }

        private async Task<OrderViewModel> GetOrderBookAsync()
        {
            var orderBook = await _orderService.GetOrderBookAsync();

            return new OrderViewModel()
            {
                OrderBookDetails = new OrderBookViewModel()
                {
                    Name = orderBook.CompanyName,
                    CurrentPrice = orderBook.CurrentPrice,
                    Asks = orderBook.Orders.Where(u => u.OrderType == false && u.OrderStatus == 0).Select(x => new PairViewModel
                        { Price = x.Price, Amount = x.Quantity, Time = x.Date }).ToList(),
                    Bids = orderBook.Orders.Where(u => u.OrderType == true && u.OrderStatus == 0).Select(x => new PairViewModel
                        { Price = x.Price, Amount = x.Quantity, Time = x.Date }).ToList(),
                }
            };
        }

        [HttpPost]
        public async Task<IActionResult> Buy(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await GetOrderBookAsync());
            }

            await _orderService.AddOrderAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), model.Buy.Price, model.Buy.Amount, true);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Sell(OrderViewModel model)
        { 
            if (!ModelState.IsValid)
                return View("Index", await GetOrderBookAsync());

            await _orderService.AddOrderAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), model.Sell.Price, model.Sell.Amount, false);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

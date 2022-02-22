﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;

        public OrderController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await ordersStorage.GetAllAsync();
            return View(orders.Select(x => x.ToOrderViewModel()).ToList());
        }
        public async Task<IActionResult> DetailsAsync(Guid orderId)
        {
            var order = await ordersStorage.TryGetByIdAsync(orderId);
            return View(order.ToOrderViewModel());
        }

        [HttpPost]
        public IActionResult UpdateStatus(Guid orderId, OrderStatusViewModel status)
        {
            ordersStorage.UpdateStatusAsync(orderId, (OrderStatus)(int)status);
            return RedirectToAction(nameof(Index));
        }
    }
}
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IOrdersStorage ordersStorage;

        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Check_out(UserDeliveryInfoViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }
            var currentCart = cartsStorage.TryGetByUserId(Constants.UserId);
            var orderDb = new Order
            {
                UserDeliveryInfo = user.ToUserDeliveryInfo(),
                Items = currentCart.Items
            };
            ordersStorage.AddAsync(orderDb);
            cartsStorage.Clear(Constants.UserId);
            return View();
        }
    }
}
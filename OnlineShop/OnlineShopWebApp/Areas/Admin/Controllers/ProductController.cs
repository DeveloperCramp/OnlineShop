﻿using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public ProductController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = productsStorage.GetAll();
            return View(Mapping.ToProductViewModels(products));
        }
        public IActionResult Edit(Guid productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(Mapping.ToProductViewModel(product));
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel product)
        {
            var productDb = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description
            };
            productsStorage.Update(productDb);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(Guid productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            var productDb = new Product
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description
            };
            productsStorage.Add(productDb);
            return RedirectToAction(nameof(Index));
        }
    }
}
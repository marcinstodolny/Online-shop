using System;
using Codecool.CodecoolShop.Helpers;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Product = Domain.Product;
using Codecool.CodecoolShop.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Codecool.CodecoolShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        [Route("index")]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            _cartService.GetProductCategories();
            _cartService.GetSuppliers();
            if (_cartService.FindProductBy(id) == null) return RedirectToAction("Index");
            if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = _cartService.FindProductBy(id), Quantity = 1 });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                var index = IsExist(id, cart);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = _cartService.FindProductBy(id), Quantity = 1 });
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);

            }

            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var index = IsExist(id, cart);
            cart.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        [Route("update/{operation}/{id}")]
        public IActionResult Update(string operation, string id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var index = IsExist(id, cart);
            if (index != -1)
            {
                switch (operation)
                {
                    case "subtract":
                        if (cart[index].Quantity > 1)
                        {
                            cart[index].Quantity--;
                        }
                        break;
                    case "add":
                        cart[index].Quantity++;
                        break;
                }
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        private int IsExist(string id, List<Item> cart)
        {
            for (var i = 0; i < cart.Count; i++)
            {

                if (cart[i].Product.Id.ToString().Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
    //public class CartController : Controller
    //{
    //    private readonly ILogger<ProductController> _logger;
    //    private CodecoolshopContext _context;

    //    public CartController(ILogger<ProductController> logger, CodecoolshopContext context)
    //    {
    //        _logger = logger;
    //        _context = context;
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //    public IActionResult Index()
    //    {

    //        //var products = ProductService.GetProductsForCategory(1);
    //        _logger.LogInformation("Opened index page");
    //        var pr = _context.Carts.ToList();
    //        return View(pr);
    //    }
    //}
}

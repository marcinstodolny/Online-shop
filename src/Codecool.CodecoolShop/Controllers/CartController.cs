using System;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Codecool.CodecoolShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private CodecoolshopContext _context;
        [Route("index")]
        public IActionResult Index(CodecoolshopContext context)
        {
            _context = context;
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {
            Product product = new Product();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = find(id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = find(id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        private Domain.Product find(string id)
        {
            var id2 = Int32.Parse(id);
            if (_context.Products.Any(product => product.Id == id2))
            {
                return _context.Products.First(product => product.Id == id2);
            }
            return null;
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

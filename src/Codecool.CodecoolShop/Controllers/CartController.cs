using Codecool.CodecoolShop.Helpers;
using Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Product = Domain.Product;

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
            if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
            {
                var cart = new List<Item> { new() { Product = Find(id), Quantity = 1 } };
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                var index = IsExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = Find(id), Quantity = 1 });
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var index = IsExist(id);
            cart.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return RedirectToAction("Index");
        }

        private int IsExist(string id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            for (var i = 0; i < cart.Count; i++)
            {

                if (cart[i].Product.Id.ToString().Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        private Product Find(string id)
        {
            if (_context.Products.Any(product => product.Id.ToString() == id))
            {
                return _context.Products.First(product => product.Id.ToString() == id);
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

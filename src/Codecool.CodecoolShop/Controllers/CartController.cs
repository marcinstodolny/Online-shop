using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

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
            LoadCart();
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            SaveCart();
            return View();
        }

        [Route("buy/{id}")]
        public IActionResult Buy(string id)
        {

            _cartService.GetProductCategories();
            _cartService.GetSuppliers();
            if (_cartService.FindProductById(id) == null) return RedirectToAction("Index");
            if (HttpContext.Session.GetObjectFromJson<List<Item>>("cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Product = _cartService.FindProductById(id), Quantity = 1 });
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
                    cart.Add(new Item { Product = _cartService.FindProductById(id), Quantity = 1 });
                }
                HttpContext.Session.SetObjectAsJson("cart", cart);

            }
            SaveCart();
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(string id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var index = IsExist(id, cart);
            cart.RemoveAt(index);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            SaveCart();
            return RedirectToAction("Index");
        }

        [Route("update/{id}/{quantity}")]
        public IActionResult Update(string id,string quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var index = IsExist(id, cart);
            var isItInt = int.TryParse(quantity, out var result);
            if (result <= 0)
            {
                return Remove(id);
            }
            if (index != -1 && isItInt)
            {
                cart[index].Quantity = result;
            }
            HttpContext.Session.SetObjectAsJson("cart", cart);
            SaveCart();
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
        [HttpPost]
        public IActionResult InputQuantity(string id)
        {
            var quantity = Request.Form["quantity"].First();
            return Update(id, quantity);
        }

        private void LoadCart()
        {
            if (!(User.Identity?.IsAuthenticated ?? false)) return;
            var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _cartService.ReadCartFromDb(userid);
            if (!cart.IsNullOrEmpty())
            {
                HttpContext.Session.SetString("cart", cart);
            }
        }
        private void SaveCart()
        {
            if (!(User.Identity?.IsAuthenticated ?? false)) return;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = HttpContext.Session.GetString("cart");
            _cartService.SaveCartToDb(userId, cart);

        }

    }
}

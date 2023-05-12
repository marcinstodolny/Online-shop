using Codecool.CodecoolShop.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Codecool.CodecoolShop.Services;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using static System.Collections.Specialized.BitVector32;

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
        [Route("index/{UserId}")]
        [Route("index")]
        public IActionResult Index(string UserId = "")
        {
            LoadCart(UserId);
            
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            SaveCart(UserId);
            return View();
        }

        [Route("buy/{id}")]
        [Route("buy/{id}/{UserId}")]
        public IActionResult Buy(string id, string UserId = "")
        {
            LoadCart(UserId);
            
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

            SaveCart(UserId);
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

        [Route("saveCart/{Userid}")]
        public IActionResult AddCartToDb(string Userid)
        {
            if (Userid.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }
            var cart = HttpContext.Session.GetString("cart");
            _cartService.SaveCartToDb(Userid, cart);
            return RedirectToAction("Index");
        }

        [Route("ReadCart/{Userid}")]
        public IActionResult ReadCartFromDb(string Userid)
        {
            if (Userid.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }
            var cart = _cartService.ReadCartFromDb(Userid);
            HttpContext.Session.SetString("cart", cart);
            return RedirectToAction("Index");
        }

        private void LoadCart(string Userid)
        {
            if (Userid.IsNullOrEmpty())
            {
                return;
            }
            var cart = _cartService.ReadCartFromDb(Userid);
            HttpContext.Session.SetString("cart", cart);
        }
        private void SaveCart(string UserId)
        {
            if (UserId.IsNullOrEmpty())
            {
                return;
            }
            var cart = HttpContext.Session.GetString("cart");
            _cartService.SaveCartToDb(UserId, cart);
        }

    }
}

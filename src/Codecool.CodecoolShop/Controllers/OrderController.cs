using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IOrderService _orderService;
        public OrderController(ILogger<ProductController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (order.ShippingSameAsBilling)
            {
                order.ShippingAddress = order.BillingAddress;
                order.ShippingCity = order.BillingCity;
                order.ShippingCountry = order.BillingCountry;
                order.ShippingZipcode = order.BillingZipcode;
            }

            if (ModelState.IsValid)
            {
                _orderService.AddOrder(order);
                return RedirectToAction("Payment");
            }

            return View("Checkout");
        }

        
    }
}

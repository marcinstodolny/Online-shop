using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Domain.Payments;

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
                _logger.LogInformation($"Checkout order completed for order id: {order.Id}");
                return RedirectToAction("Payment");
            }
            _logger.LogInformation($"Checkout order not valid for order id: {order.Id}");
            return View("Checkout");
        }

        public IActionResult Payment()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
            var totalPrice = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            ViewBag.TotalPrice = totalPrice;
            return View();
        }

        [HttpPost]
        public IActionResult Payment(CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Payment details are correct for the credit card number {creditCard.CardNumber}");
                return RedirectToAction("Confirmation");
            }
            _logger.LogInformation($"Payment details are incorrect or invalid for the credit card number {creditCard.CardNumber}");
            return RedirectToAction("Payment");
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}

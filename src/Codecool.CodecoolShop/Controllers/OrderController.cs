using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IActionResult Checkout([Bind("Name,Description,Email,Phone,BillingCountry,BillingCity,BillingZipcode,BillingAddress,ShippingCountry,ShippingCity,ShippingZipcode,ShippingAddress")] Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.AddOrder(order);
                return RedirectToAction("Payment");
            }

            return View("Checkout");
        }

        
    }
}

using System;
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
            try
            {
                if (order.ShippingSameAsBilling)
                {
                    order.ShippingAddress = order.BillingAddress;
                    order.ShippingCity = order.BillingCity;
                    order.ShippingCountry = order.BillingCountry;
                    order.ShippingZipcode = order.BillingZipcode;
                }

                order.CreatedAt = DateTime.Now;
                order.Status = "Checked";

                if (ModelState.IsValid)
                {
                    order.CreatedAt = DateTime.Now;
                    order.Status = "Confirmed";
                    HttpContext.Session.SetObjectAsJson("orderDetails", order);
                    _orderService.SaveOrderToJson(order);
                    _orderService.AddOrder(order);

                    _logger.LogInformation($"Checkout order completed for order id: {order.Id}");
                    return RedirectToAction("Payment");
                }

                _logger.LogInformation($"Checkout order not valid for order id: {order.Id}");
                _orderService.AddOrder(order);

                return View("Checkout");
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during the checkout process: {e.Message}");
            }
            
            return View("Checkout");
        }

        public IActionResult Payment()
        {
            try
            {
                var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart");
                var totalPrice = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
                ViewBag.TotalPrice = totalPrice;
                ViewBag.paymentMessage = HttpContext.Session.GetObjectFromJson<string>("paymentMessage");
                HttpContext.Session.Remove("paymentMessage");

                return View();
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred while loading the payment page: {e.Message}");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Payment(CreditCard creditCard)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    _logger.LogInformation($"Payment details are correct for the credit card number {creditCard.CardNumber}");
                    return RedirectToAction("Confirmation");
                }
                var order = HttpContext.Session.GetObjectFromJson<Order>("orderDetails");

                order.CreatedAt = DateTime.Now;
                order.Status = "Failed payment";
                _orderService.SaveOrderToJson(order);

                _orderService.AddOrder(order);

                var lastFourDigits = 4;
                var paymentMessage =
                    $"Payment details are incorrect or invalid for the credit card ending number " +
                    $"{creditCard.CardNumber.Substring(creditCard.CardNumber.Length - lastFourDigits)}";

                _logger.LogInformation($"Payment details are incorrect or invalid for the credit card number {creditCard.CardNumber}");
                HttpContext.Session.SetObjectAsJson("paymentMessage", paymentMessage);

                return RedirectToAction("Payment");
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during the payment process: {e.Message}");
            }

            return RedirectToAction("Payment");
        }

        public IActionResult Confirmation()
        {
            try
            {
                var order = HttpContext.Session.GetObjectFromJson<Order>("orderDetails");
                order.CreatedAt = DateTime.Now;
                order.Status = "Payed";
                _orderService.SaveOrderToJson(order);

                var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);

                _logger.LogInformation($"Order id {order.Id} has been payed for");

                HttpContext.Session.Remove("cart");
                HttpContext.Session.Remove("orderDetails");

                // Send the email confirmation
                //_orderService.SendEmailConfirmation(order, ViewBag.total); //; this is an example to configure SMTP client instance to send confirmation email

                _orderService.AddOrder(order);

                return View(order);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occurred during the confirmation process: {e.Message}");
            }

            return RedirectToAction("Payment");
        }
    }

}

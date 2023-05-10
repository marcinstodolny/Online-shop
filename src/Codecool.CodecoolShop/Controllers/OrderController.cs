using System;
using Codecool.CodecoolShop.Helpers;
using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Domain.Payments;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace Codecool.CodecoolShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private IOrderService _orderService;
        private IWebHostEnvironment _hostingEnvironment;

        public OrderController(ILogger<ProductController> logger, IOrderService orderService, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _orderService = orderService;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            order.CreatedAt = DateTime.Now;
            order.Status = "Checked";
            SaveOrderToJson(order);

            if (order.ShippingSameAsBilling)
            {
                order.ShippingAddress = order.BillingAddress;
                order.ShippingCity = order.BillingCity;
                order.ShippingCountry = order.BillingCountry;
                order.ShippingZipcode = order.BillingZipcode;
            }
            if (ModelState.IsValid)
            {
                order.CreatedAt = DateTime.Now;
                order.Status = "Confirmed";
                SaveOrderToJson(order);

                HttpContext.Session.SetObjectAsJson("orderDetails", order);
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
            ViewBag.paymentMessage = HttpContext.Session.GetObjectFromJson<string>("paymentMessage");
            HttpContext.Session.Remove("paymentMessage");

            return View();
        }

        [HttpPost]
        public IActionResult Payment(CreditCard creditCard)
        {
            if (ModelState.IsValid)
            {

                _logger.LogError($"Payment details are correct for the credit card number {creditCard.CardNumber}");
                return RedirectToAction("Confirmation");
            }
            var order = HttpContext.Session.GetObjectFromJson<Order>("orderDetails");

            order.CreatedAt = DateTime.Now;
            order.Status = "Failed payment";
            SaveOrderToJson(order);

            var lastFourDigits = 4;
            var paymentMessage =
                $"Payment details are incorrect or invalid for the credit card ending number {creditCard.CardNumber.Substring(creditCard.CardNumber.Length - lastFourDigits)}";
            _logger.LogError($"Payment details are incorrect or invalid for the credit card number {creditCard.CardNumber}");
            HttpContext.Session.SetObjectAsJson("paymentMessage", paymentMessage);
            return RedirectToAction("Payment");
        }

        public IActionResult Confirmation()
        {
            var order = HttpContext.Session.GetObjectFromJson<Order>("orderDetails");
            order.CreatedAt = DateTime.Now;
            order.Status = "Payed";
            SaveOrderToJson(order);

            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>("cart") ?? new List<Item>();
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(item => item.Product.DefaultPrice * item.Quantity);
            //var order = _orderService.GetAllOrders();

            HttpContext.Session.Remove("cart");
            HttpContext.Session.Remove("orderDetails");

            // Send the email confirmation
            // SendEmailConfirmation(order); this is an example to configure SMTP client instance to send confirmation email

            _orderService.AddOrder(order);

            return View(order);
        }

        private void SendEmailConfirmation(Order order)
        {
            // Create a new SmtpClient instance with your SMTP server details
            var client = new SmtpClient("test.smtp.server.com", 587)
            {
                Credentials = new NetworkCredential("test-username", "test-password"),
                EnableSsl = true
            };

            // Create a new MailMessage instance
            var message = new MailMessage();

            // Set the sender and recipient email addresses
            message.From = new MailAddress("test@codecoolshop.com");
            message.To.Add(new MailAddress(order.Email));

            // Set the subject and body of the email
            message.Subject = "Order Confirmation";
            message.Body = $"Dear {order.Name},\n\nThank you for your order. Your order has been confirmed.\n\nShipping Address:\n{order.ShippingAddress}\n\nTotal amount: {ViewBag.total:C}\n\nBest regards,\nYour Company";

            // Send the email using the SmtpClient
            client.Send(message);
        }
        private void SaveOrderToJson(Order order)
        {
            var orderId = order.Id;
            var currentDate = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
            var fileName = $"{order.Status}_{currentDate}.json";
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "OrderJsonLogFiles", fileName);

            // Create the directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // Serialize the order object to JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            var orderJson = JsonSerializer.Serialize(order, options);

            // Write the JSON data to the file using FileStream
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(orderJson);
            }
        }
    }

}

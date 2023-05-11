using Data;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Codecool.CodecoolShop.Services
{
    public class OrderService : IOrderService
    {
        private CodecoolshopContext _context;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly EmailContext _emailContext;

        public OrderService(CodecoolshopContext context, IWebHostEnvironment hostingEnvironment, IOptions<EmailContext> emailContext)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _emailContext = emailContext.Value;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Domain.Order> GetAllOrders() => _context.Orders.ToList();

        public void SendEmailConfirmation(Order order, string total)
        {
            // Create a new SmtpClient instance with your SMTP server details
            var client = new SmtpClient(_emailContext.Smtp, _emailContext.Port)
            {
                Credentials = new NetworkCredential(_emailContext.Username, _emailContext.Password),
                EnableSsl = true
            };

            // Create a new MailMessage instance
            var message = new MailMessage();

            // Set the sender and recipient email addresses
            message.From = new MailAddress(_emailContext.Address);
            message.To.Add(new MailAddress(order.Email));

            // Set the subject and body of the email
            message.Subject = "Order Confirmation";
            message.Body = $"Dear {order.Name},\n\nThank you for your order. Your order has been confirmed.\n\nShipping Address:\n{order.ShippingAddress}\n\nTotal amount: {total}\n\nBest Regards,\nYour CodeCoolShop";

            // Send the email using the SmtpClient
            client.Send(message);
        }
        public void SaveOrderToJson(Order order)
        {
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

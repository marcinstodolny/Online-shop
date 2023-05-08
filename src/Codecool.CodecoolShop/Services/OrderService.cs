using Codecool.CodecoolShop.Controllers;
using Data;
using Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Codecool.CodecoolShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<ProductController> _logger;
        private CodecoolshopContext _context;

        public OrderService(ILogger<ProductController> logger, CodecoolshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public List<Domain.Order> GetAllOrders() => _context.Orders.ToList();
    }
}

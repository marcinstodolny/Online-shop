using Codecool.CodecoolShop.Controllers;
using Data;
using Domain;
using Microsoft.Extensions.Logging;

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
    }
}

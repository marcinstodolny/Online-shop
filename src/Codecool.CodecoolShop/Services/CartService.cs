using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Controllers;
using Data;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Services
{
    public class CartService : ICartService
    {

        private readonly ILogger<CartController> _logger;
        private ICodecoolshopContext _context;

        public CartService(ILogger<CartController> logger, ICodecoolshopContext context)
        {
            _logger = logger;
            _context = context;
            CodecoolshopContext.IfDbEmptyAddNewItems(context);
        }

        public List<Domain.Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public List<Domain.ProductCategory> GetProductCategories()
        {
            var productCategories = _context.ProductCategories.ToList();
            return productCategories;
        }

        public List<Domain.Supplier> GetSuppliers()
        {
            var suppliers = _context.Suppliers.ToList();
            return suppliers;
        }

        public List<Domain.Product> GetProductsByCategory(int categoryId)
        {
            var products = _context.Products.Where(p => p.ProductCategory.Id == categoryId).ToList();
            return products;
        }

        public List<Domain.Product> GetProductsBySupplier(int supplierId)
        {
            var products = _context.Products.Where(p => p.Supplier.Id == supplierId).ToList();
            return products;
        }

        public Domain.Product FindProductBy(string id)
        {
            if (_context.Products.Any(product => product.Id.ToString() == id))
            {
                return _context.Products.First(product => product.Id.ToString() == id);
            }

            return null;
        }
    }
}

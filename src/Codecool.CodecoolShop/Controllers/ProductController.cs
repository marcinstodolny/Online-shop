using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private CodecoolshopContext _context;
        private IProductServiceSql _productServiceSql;
        public ProductController(ILogger<ProductController> logger, IProductServiceSql productServiceSql)
        {
            _logger = logger;
            //ProductService = new ProductService(
            //    ProductDaoMemory.GetInstance(),
            //    ProductCategoryDaoMemory.GetInstance());
            _productServiceSql = productServiceSql;
            _context = _productServiceSql.GetContext();
            _context.IfDbEmptyAddNewItems(_context);
        }

        public IActionResult Index()
        {
            //var products = ProductService.GetProductsForCategory(1);
            _logger.LogInformation("Opened index page");
            var pr = _context.Products.ToList();
            _context.ProductCategories.ToList();
            _context.Suppliers.ToList();
            return View(pr);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

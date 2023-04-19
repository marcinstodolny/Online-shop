using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private CodecoolshopContext _context;

        //public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, CodecoolshopContext context)
        {
            _logger = logger;
            _context = context;
            //ProductService = new ProductService(
            //    ProductDaoMemory.GetInstance(),
            //    ProductCategoryDaoMemory.GetInstance());
            context.IfDbEmptyAddNewItems(context);
        }

        public IActionResult Index()
        {
            //var products = ProductService.GetProductsForCategory(1);
            _logger.LogInformation("Opened index page");
            var pr = _context.Products.ToList();
            var prCat = _context.ProductCategories.ToList();
            var sup = _context.Suppliers.ToList();
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

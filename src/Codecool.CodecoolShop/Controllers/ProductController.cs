using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Data;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private CodecoolshopContext _context;

        private IProductCategoryDao _product;
        //public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, CodecoolshopContext context, IProductCategoryDao product)
        {
            _logger = logger;
            _context = context;
            _product = product;
            //ProductService = new ProductService(
            //    ProductDaoMemory.GetInstance(),
            //    ProductCategoryDaoMemory.GetInstance());
        }

        public IActionResult Index()
        {
            //var products = ProductService.GetProductsForCategory(1);
            _logger.LogInformation("Opened index page");
            var pr = _context.Products.ToList();
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

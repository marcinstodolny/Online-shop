using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Controllers;
using Data;
using Microsoft.Extensions.Logging;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService : IProductService
    {

        private readonly ILogger<ProductController> _logger;
        private CodecoolshopContext _context;

        public ProductService(ILogger<ProductController> logger, CodecoolshopContext context)
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

        // ** Service only for in memory database **

        //    private readonly Daos.IProductDao productDao;
        //    private readonly Daos.IProductCategoryDao productCategoryDao;
        //    private readonly ILogger<ProductService> _logger;
        //    private CodecoolshopContext _context;


        //    public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ILogger<ProductService> logger, CodecoolshopContext context)
        //    {
        //        _logger = logger;
        //        _context = context;
        //        this.productDao = productDao;
        //        this.productCategoryDao = productCategoryDao;
        //    }

        //    public List<Domain.Product> GetAllProducts()
        //    {
        //        var products = _context.Products.ToList();
        //        return products;
        //    }

        //    public Models.ProductCategory GetProductCategory(int categoryId)
        //    {
        //        return this.productCategoryDao.Get(categoryId);
        //    }

        //    public IEnumerable<CodecoolShop.Models.Product> GetProductsForCategory(int categoryId)
        //    {
        //        Models.ProductCategory category = this.productCategoryDao.Get(categoryId);
        //        return this.productDao.GetBy(category);
        //    }
        //}
    }
}

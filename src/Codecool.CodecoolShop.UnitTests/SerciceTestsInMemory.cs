using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Services;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NSubstitute.ExceptionExtensions;

namespace Codecool.CodecoolShop.UnitTests
{
    public class ProductServiceTestsInMemory
    {
        private Mock<ILogger<ProductController>> _mockLoggerProduct;
        private Mock<ILogger<CartController>> _mockLoggerCart;
        private Mock<DbSet<Product>> _mockProducts;
        private Mock<CodecoolshopContext> _mockContext;
        private DbSet<Product> _dbSet;
        private CodecoolshopContext _context;
        private DbContextOptions<CodecoolshopContext> _options;
        private ProductService _productService;
        private CartService _cartService;

        //test data
        private Product _tabletProduct;
        private Product _laptopProduct;
        private ProductCategory _tablet;
        private ProductCategory _laptop;
        private ProductCategory _phone;
        private Supplier _amazon;
        private Supplier _microsoft;
        private Supplier _allegro;


        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _options = new DbContextOptionsBuilder<CodecoolshopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockLoggerProduct = new Mock<ILogger<ProductController>>();
            _mockLoggerCart = new Mock<ILogger<CartController>>();
            _mockProducts = new Mock<DbSet<Product>>();
            _mockContext = new Mock<CodecoolshopContext>();

            _context = new CodecoolshopContext(_options, config);
            _dbSet = _context.Set<Product>();
            AddTestProductsToDatabase();
            _productService = new ProductService(_mockLoggerProduct.Object, _context);
            _cartService = new CartService(_mockLoggerCart.Object, _context);
        }


        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            // Act
            var allProducts = _productService.GetAllProducts();

            // Assert
            Assert.AreEqual(_context.Products.ToList().Count, allProducts.Count);
        }

        [Test]
        public void GetProductCategories_ReturnsExpectedCategoryCount()
        {
            // Act
            var allCategories = _productService.GetProductCategories();

            // Assert
            Assert.AreEqual(_context.ProductCategories.ToList().Count, allCategories.Count);
        }

        [Test]
        public void GetSuppliers_ReturnsExpectedSupplierCount()
        {
            // Act
            var allSuppliers = _productService.GetSuppliers();

            // Assert
            Assert.AreEqual(_context.Suppliers.ToList().Count, allSuppliers.Count);
        }

        [Test]
        public void GetProductsByCategory_ReturnsExpectedProductCount()
        {
            // Act
            var allProducts = _productService.GetProductsByCategory(_productService.GetProductCategories().First(category => category == _laptop).Id);

            // Assert
            Assert.AreEqual(_context.Products.Count(product => product.ProductCategory == _laptop), allProducts.Count);
        }

        [Test]
        public void GetProductsBySupplier_ReturnsExpectedProductCount()
        {
            // Act
            var allProducts = _productService.GetProductsBySupplier(_productService.GetSuppliers().First(supplier => supplier == _amazon).Id);

            // Assert
            Assert.AreEqual(_context.Products.Count(product => product.Supplier == _amazon), allProducts.Count);
        }

        [Test]
        public void GetProductsByCategory_NoMatchingCategories_ReturnsEmptyList()
        {
            // Act
            var allProducts = _productService.GetProductsByCategory(_productService.GetProductCategories().First(category => category == _phone).Id);

            // Assert
            Assert.IsEmpty(allProducts);
        }

        [Test]
        public void GetProductsBySupplier_NoMatchingSuppliers_ReturnsEmptyList()
        {
            // Act
            var allProducts = _productService.GetProductsBySupplier(_productService.GetSuppliers().First(supplier => supplier == _allegro).Id);

            // Assert
            Assert.IsEmpty(allProducts);
        }
        [Test]
        public void GetProductsById_ReturnProduct()
        {
            // Act
            var product = _cartService.FindProductById(
                _productService.GetAllProducts()
                .Select(product => product.Id)
                .First().ToString());

            // Assert
            Assert.AreEqual(product, _productService.GetAllProducts().First());
        }

        [Test]
        public void GetProductsById_WrongIdThrowError()
        {
            //act + assert
            Assert.Throws<ArgumentException>(() => _cartService.FindProductById("wrong"));
        }

        [Test]
        public void GetSuppliers_ReturnsExpectedSuppliers()
        {
            // Act
            var allSuppliers = _productService.GetSuppliers();
            var suppliers = _context.Suppliers.ToList();
            
            // Assert
            for (var i = 0; i < allSuppliers.Count; i++)
            {
                Assert.AreEqual(allSuppliers[i], suppliers[i]);
            }
        }

        private void AddTestProductsToDatabase()
        {
            //supplier
            _amazon = new Supplier { Name = "Amazon", Description = "test" };
            _microsoft = new Supplier { Name = "Microsoft", Description = "test" };
            _allegro = new Supplier { Name = "Allegro", Description = "test" };
            _context.Suppliers.Add(_amazon);
            _context.Suppliers.Add(_microsoft);
            _context.Suppliers.Add(_allegro);
            //product categories
            _tablet = new ProductCategory { Name = "Tablet", Department = "Hardware", Description = "WIP" };
            _laptop = new ProductCategory { Name = "Laptop", Department = "Hardware", Description = "WIP" };
            _phone = new ProductCategory { Name = "Phone", Department = "Hardware", Description = "WIP" };
            _context.ProductCategories.Add(_tablet);
            _context.ProductCategories.Add(_laptop);
            _context.ProductCategories.Add(_phone);
            //products
            _tabletProduct = new Product
            {
                Name = "tablet",
                DefaultPrice = 49.9m,
                Currency = "USD",
                Description = "test",
                ProductCategory = _tablet,
                Supplier = _amazon
            };
            _laptopProduct = new Product
            {
                Name = "laptop",
                DefaultPrice = 479.0m,
                Currency = "USD",
                Description = "test",
                ProductCategory = _laptop,
                Supplier = _microsoft
            };
            _context.Products.Add(_tabletProduct);
            _context.Products.Add(_laptopProduct);
            _context.SaveChanges();
        }
    }
}
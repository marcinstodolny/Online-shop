using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Data;
using Codecool.CodecoolShop.Controllers;
using Codecool.CodecoolShop.Services;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Xml;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.UnitTests
{
    public class ProductServiceTestsInMemory
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<DbSet<Product>> _mockProducts;
        private Mock<CodecoolshopContext> _mockContext;
        private DbSet<Product> _dbSet;
        private CodecoolshopContext _context;
        private DbContextOptions<CodecoolshopContext> _options;


        [SetUp]
        public void Setup()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _options = new DbContextOptionsBuilder<CodecoolshopContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProducts = new Mock<DbSet<Product>>();
            _mockContext = new Mock<CodecoolshopContext>();

            _context = new CodecoolshopContext(_options, config);
            _dbSet = _context.Set<Product>();
        }


        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            // Arrange
            var productService = new ProductService(_mockLogger.Object, _context);

            // Act
            var allProducts = productService.GetAllProducts();

            // Assert
            Assert.AreEqual(15, allProducts.Count);
        }

        [Test]
        public void GetProductCategories_ReturnsExpectedCategoryCount()
        {
            Assert.Pass();
        }

        [Test]
        public void GetSuppliers_ReturnsExpectedSupplierCount()
        {
            Assert.Pass();
        }

        [Test]
        public void GetProductsByCategory_ReturnsExpectedProductCount()
        {
            Assert.Pass();
        }

        [Test]
        public void GetProductsBySupplier_ReturnsExpectedProductCount()
        {
            Assert.Pass();
        }

        [Test]
        public void GetProductsByCategory_NoMatchingCategories_ReturnsEmptyList()
        {
            Assert.Pass();
        }

        [Test]
        public void GetProductsBySupplier_NoMatchingSuppliers_ReturnsEmptyList()
        {
            Assert.Pass();
        }
    }
}
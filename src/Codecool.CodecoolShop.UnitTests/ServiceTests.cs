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

namespace Codecool.CodecoolShop.UnitTests
{
    public class ProductServiceTests
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<DbSet<Product>> _mockProducts;
        private Mock<ICodecoolshopContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProducts = new Mock<DbSet<Product>>();
            _mockContext = new Mock<ICodecoolshopContext>();
        }


        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product(),
                new Product()
            }.AsQueryable();

            _mockProducts.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            _mockProducts.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            _mockProducts.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            _mockProducts.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            _mockContext.Setup(c => c.Products).Returns(_mockProducts.Object);

            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allProducts = productService.GetAllProducts();

            // Assert
            Assert.AreEqual(2, allProducts.Count);
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
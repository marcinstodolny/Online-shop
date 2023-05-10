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
using System;

namespace Codecool.CodecoolShop.UnitTests
{
    public class ProductServiceTests
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<DbSet<Product>> _mockProducts;
        private Mock<DbSet<ProductCategory>> _mockCategories;
        private Mock<ICodecoolshopContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProducts = new Mock<DbSet<Product>>();
            _mockCategories = new Mock<DbSet<ProductCategory>>();
            _mockContext = new Mock<ICodecoolshopContext>();
        }

        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { },
                new Product { }
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
            Assert.That(allProducts, Has.Count.EqualTo(2));
        }

        [Test]
        public void GetAllProducts_ReturnsExpectedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
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
            var productList = products.ToList();
            for (int i = 0; i < productList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(allProducts[i].Id, Is.EqualTo(productList[i].Id));
                    Assert.That(allProducts[i].Name, Is.EqualTo(productList[i].Name));
                });
            }
        }

        [Test]
        public void GetProductCategories_ReturnsExpectedCategoryCount()
        {
            // Arrange
            var categories = new List<ProductCategory>
            {
                new ProductCategory { },
                new ProductCategory { },
                new ProductCategory { }
            }.AsQueryable();

            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.Provider).Returns(categories.Provider);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.Expression).Returns(categories.Expression);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

            _mockContext.Setup(c => c.ProductCategories).Returns(_mockCategories.Object);
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allCategories = productService.GetProductCategories();

            // Assert
            Assert.That(allCategories, Has.Count.EqualTo(3));
        }

        [Test]
        public void GetProductCategories_ReturnsExpectedCategories()
        {
            // Arrange
            var categories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1, Name = "Category 1" },
                new ProductCategory { Id = 2, Name = "Category 2" },
                new ProductCategory { Id = 2, Name = "Category 3" }
            }.AsQueryable();

            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.Provider).Returns(categories.Provider);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.Expression).Returns(categories.Expression);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.ElementType).Returns(categories.ElementType);
            _mockCategories.As<IQueryable<ProductCategory>>().Setup(m => m.GetEnumerator()).Returns(categories.GetEnumerator());

            _mockContext.Setup(c => c.ProductCategories).Returns(_mockCategories.Object);
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allCategories = productService.GetProductCategories();

            // Assert
            Assert.That(allCategories, Has.Count.EqualTo(3));
            var categoriesList = categories.ToList();
            for (int i = 0; i < categoriesList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(allCategories[i].Id, Is.EqualTo(categoriesList[i].Id));
                    Assert.That(allCategories[i].Name, Is.EqualTo(categoriesList[i].Name));
                });
            }
        }

        [Test]
        public void GetSuppliers_ReturnsExpectedSupplierCount()
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
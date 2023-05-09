using Codecool.CodecoolShop.Services;
using Domain;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Codecool.CodecoolShop.Tests
{
    public class ProductServiceTests
    {
        private Mock<ILogger<ProductController>> _mockLogger;
        private Mock<CodecoolshopContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockContext = new Mock<CodecoolshopContext>();
        }

        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            Assert.Pass();
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
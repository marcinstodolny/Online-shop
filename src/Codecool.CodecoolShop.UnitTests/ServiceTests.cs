using Moq;
using Microsoft.Extensions.Logging;
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
        private Mock<DbSet<ProductCategory>> _mockCategories;
        private Mock<DbSet<Supplier>> _mockSuppliers;
        private Mock<ICodecoolshopContext> _mockContext;

        private void SetupMockContext<T>(Mock<DbSet<T>> mockSet, IQueryable<T> data) where T : class
        {
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<ProductController>>();
            _mockProducts = new Mock<DbSet<Product>>();
            _mockCategories = new Mock<DbSet<ProductCategory>>();
            _mockSuppliers = new Mock<DbSet<Supplier>>();
            _mockContext = new Mock<ICodecoolshopContext>();

            SetupMockContext(_mockProducts, new List<Product>{ }.AsQueryable());

            SetupMockContext(_mockCategories, new List<ProductCategory>{ }.AsQueryable());

            SetupMockContext(_mockSuppliers, new List<Supplier>{ }.AsQueryable());

            _mockContext.Setup(c => c.Products).Returns(_mockProducts.Object);
            _mockContext.Setup(c => c.ProductCategories).Returns(_mockCategories.Object);
            _mockContext.Setup(c => c.Suppliers).Returns(_mockSuppliers.Object);
        }

        [Test]
        public void GetAllProducts_ReturnsExpectedProductCount()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1"},
                new Product { Id = 2, Name = "Product 2", Description = "Description 2"}
            }.AsQueryable();

            SetupMockContext(_mockProducts, products);
            
            var productList = products.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);
            
            // Act
            var allProducts = productService.GetAllProducts();

            // Assert
            Assert.That(allProducts, Has.Count.EqualTo(productList.Count));
        }

        [Test]
        public void GetAllProducts_ReturnsExpectedProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1"},
                new Product { Id = 2, Name = "Product 2", Description = "Description 2"}
            }.AsQueryable();

            SetupMockContext(_mockProducts, products);

            var productList = products.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allProducts = productService.GetAllProducts();

            // Assert
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
                new ProductCategory { Id = 1, Name = "Category 1", Description = "Description 1" },
                new ProductCategory { Id = 2, Name = "Category 2", Description = "Description 2" }
            }.AsQueryable();

            SetupMockContext(_mockCategories, categories);

            var categoriestList = categories.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allCategories = productService.GetProductCategories();

            // Assert
            Assert.That(allCategories, Has.Count.EqualTo(categoriestList.Count));
        }

        [Test]
        public void GetProductCategories_ReturnsExpectedCategories()
        {
            // Arrange
            var categories = new List<ProductCategory>
            {
                new ProductCategory { Id = 1, Name = "Category 1", Description = "Description 1" },
                new ProductCategory { Id = 2, Name = "Category 2", Description = "Description 2" }
            }.AsQueryable();

            SetupMockContext(_mockCategories, categories);

            var categoriesList = categories.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allCategories = productService.GetProductCategories();

            // Assert
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
            // Arrange
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "Supplier 1", Description = "Description 1" },
                new Supplier { Id = 2, Name = "Supplier 2", Description = "Description 2" }
            }.AsQueryable();

            SetupMockContext(_mockSuppliers, suppliers);

            var supplierList = suppliers.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allSuppliers = productService.GetSuppliers();

            // Assert
            Assert.That(allSuppliers, Has.Count.EqualTo(supplierList.Count));
        }

        [Test]
        public void GetSuppliers_ReturnsExpectedSuppliers()
        {
            // Arrange
            var suppliers = new List<Supplier>
            {
                new Supplier { Id = 1, Name = "Supplier 1", Description = "Description 1" },
                new Supplier { Id = 2, Name = "Supplier 2", Description = "Description 2" }
            }.AsQueryable();

            SetupMockContext(_mockSuppliers, suppliers);

            var supplierList = suppliers.ToList();
            var productService = new ProductService(_mockLogger.Object, _mockContext.Object);

            // Act
            var allSuppliers = productService.GetSuppliers();

            // Assert
            for (int i = 0; i < supplierList.Count; i++)
            {
                Assert.Multiple(() =>
                {
                    Assert.That(allSuppliers[i].Id, Is.EqualTo(supplierList[i].Id));
                    Assert.That(allSuppliers[i].Name, Is.EqualTo(supplierList[i].Name));
                });
            }
        }

        [Test]
        public void GetProductsByCategory_NoMatchingCategories_ReturnsEmptyList()
        {
        }

        [Test]
        public void GetProductsBySupplier_NoMatchingSuppliers_ReturnsEmptyList()
        {
        }

    }
}
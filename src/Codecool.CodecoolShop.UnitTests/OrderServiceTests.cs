using Codecool.CodecoolShop.Services;
using Data;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Text.Json;

namespace Codecool.CodecoolShop.UnitTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService orderService;
        private Mock<IWebHostEnvironment> hostingEnvironmentMock;
        private string contentRootPath;
        private string testDirectoryPath;

        [SetUp]
        public void Setup()
        {
            contentRootPath = AppDomain.CurrentDomain.BaseDirectory;
            hostingEnvironmentMock = new Mock<IWebHostEnvironment>();
            hostingEnvironmentMock.Setup(h => h.ContentRootPath).Returns(contentRootPath);

            orderService = new OrderService(Mock.Of<CodecoolshopContext>(), hostingEnvironmentMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            if (!string.IsNullOrEmpty(testDirectoryPath) && Directory.Exists(testDirectoryPath))
            {
                Directory.Delete(testDirectoryPath, true);
            }
        }

        [Test]
        public void SaveOrderToJson_Should_CreateDirectoryAndWriteJsonToFile()
        {
            // Arrange
            var order = new Order { Status = "Completed" };

            // Act
            orderService.SaveOrderToJson(order);

            // Assert
            var expectedDirectory = Path.Combine(contentRootPath, "OrderJsonLogFiles");
            testDirectoryPath = expectedDirectory; // Store the path for teardown
            Assert.That(Directory.Exists(expectedDirectory), Is.True);

            var expectedFilePath = Path.Combine(expectedDirectory, $"{order.Status}_*.json");
            Assert.That(Directory.GetFiles(expectedDirectory, Path.GetFileName(expectedFilePath)).Length, Is.EqualTo(1));
        }
    }

}

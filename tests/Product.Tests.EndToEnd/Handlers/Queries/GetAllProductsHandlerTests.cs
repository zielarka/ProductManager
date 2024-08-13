using Moq;
using NUnit.Framework;
using ProductManager.Application.Handlers;
using ProductManager.Application.Queries;
using ProductManager.Core.Entities;
using ProductManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Tests.EndToEnd.Handlers.Queries
{

    [TestFixture]
    public class GetAllProductsHandlerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private GetAllProductsHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetAllProductsHandler(_productRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnListOfProductResponses()
        {
            // Arrange
            var productsList = new List<Product>
        {
            new Product(Guid.NewGuid(), "Product 1", "P001", 50.0m, DateTime.Now),
            new Product(Guid.NewGuid(), "Product 2", "P002", 100.0m, DateTime.Now)
        };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync())
                .ReturnsAsync(productsList);

            var query = new GetAllProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Product 1", result[0].Name);
            Assert.AreEqual("P001", result[0].Code);
            Assert.AreEqual(50.0m, result[0].Price);

            Assert.AreEqual("Product 2", result[1].Name);
            Assert.AreEqual("P002", result[1].Code);
            Assert.AreEqual(100.0m, result[1].Price);

            _productRepositoryMock.Verify(repo => repo.GetProductsAsync(), Times.Once);
        }
    }
}

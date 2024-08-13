using Moq;
using NUnit.Framework;
using ProductManager.Application.Commands;
using ProductManager.Core.Entities;
using ProductManager.Core.Repositories;
using ProductManagers.Application.Handlers;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProductManager.Application.Extensions;

namespace ProductManager.Tests.EndToEnd
{
    [TestFixture]
    public class CreateProductCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private CreateProductCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new CreateProductCommandHandler(_productRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateProductAndReturnProductResponse()
        {
            // Arrange
            var createProductCommand = new CreateProductCommand
            {
                Name = "Test Product",
                Code = "TP123",
                Price = 100.00m
            };

            var productEntity = new Product(Guid.NewGuid(), createProductCommand.Name, createProductCommand.Code, createProductCommand.Price, DateTime.Now);

            // Zakładamy, że produkt nie istnieje, więc możemy kontynuować
            _productRepositoryMock
                .Setup(repo => repo.GetOrFailAsyncIfExists(It.IsAny<string>()))
                .ReturnsAsync(productEntity);


            _productRepositoryMock
                .Setup(repo => repo.CreateProductAsync(It.IsAny<Product>()))
                .ReturnsAsync(productEntity);

            // Act
            var result = await _handler.Handle(createProductCommand, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(createProductCommand.Name, result.Name);
            Assert.AreEqual(createProductCommand.Code, result.Code);
            Assert.AreEqual(createProductCommand.Price, result.Price);

            _productRepositoryMock.Verify(repo => repo.GetOrFailAsyncIfExists(createProductCommand.Name), Times.Once);
            _productRepositoryMock.Verify(repo => repo.CreateProductAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}

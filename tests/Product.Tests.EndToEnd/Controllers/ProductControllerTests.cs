using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ProductManager.API.Controllers;
using ProductManager.Application.Commands;
using ProductManager.Application.Queries;
using ProductManager.Application.Responses;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManager.Tests.EndToEnd.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private ProductController _controller;

        [SetUp]
        public void SetUp()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductController(_mediatorMock.Object);
        }

        [Test]
        public async Task GetAllProductManagers_ShouldReturnOkResultWithProductsList()
        {
            // Arrange
            var productsResponseList = new List<ProductResponse>
            {
                new ProductResponse { Name = "Product 1", Code = "P001", Price = 50.0m },
                new ProductResponse { Name = "Product 2", Code = "P002", Price = 100.0m }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productsResponseList);

            // Act
            var result = await _controller.GetAllProductManagers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(productsResponseList, okResult.Value);
        }

        [Test]
        public async Task CreateProductManager_ShouldReturnOkResultWithCreatedProduct()
        {
            // Arrange
            var createProductCommand = new CreateProductCommand
            {
                Name = "New Product",
                Code = "NP001",
                Price = 75.0m
            };

            var productResponse = new ProductResponse
            {
                Name = createProductCommand.Name,
                Code = createProductCommand.Code,
                Price = createProductCommand.Price
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(productResponse);

            // Act
            var result = await _controller.CreateProductManager(createProductCommand);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual((int)HttpStatusCode.OK, okResult.StatusCode);
            Assert.AreEqual(productResponse, okResult.Value);
        }
    }
}


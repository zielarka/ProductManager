using System;
using NUnit.Framework;
using ProductManager.Core.Entities;

namespace ProductManager.Tests.Domain
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void Product_Creates_With_Valid_Parameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Test Product";
            var code = "TP123";
            var price = 10.99m;
            var createdAt = DateTime.UtcNow;

            // Act
            var product = new Product(id, name, code, price, createdAt);

            // Assert
            Assert.AreEqual(id, product.Id);
            Assert.AreEqual(name.ToLowerInvariant(), product.Name);
            Assert.AreEqual(code.ToLowerInvariant(), product.Code);
            Assert.AreEqual(price, product.Price);
            Assert.AreEqual(createdAt, product.CreatedAt);
        }

        [Test]
        public void SetName_Updates_Name_And_UpdatedAt()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Initial Product";
            var code = "IP123";
            var price = 10.99m;
            var createdAt = DateTime.UtcNow;
            var product = new Product(id, name, code, price, createdAt);

            var newName = "Updated Product";
            var beforeUpdate = DateTime.UtcNow;

            // Act
            product.SetName(newName);

            // Assert
            Assert.AreEqual(newName.ToLowerInvariant(), product.Name);
            Assert.IsTrue(product.UpdatedAt >= beforeUpdate);
        }

        [Test]
        public void SetCode_Updates_Code_And_UpdatedAt()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Initial Product";
            var code = "IP123";
            var price = 10.99m;
            var createdAt = DateTime.UtcNow;
            var product = new Product(id, name, code, price, createdAt);

            var newCode = "UP456";
            var beforeUpdate = DateTime.UtcNow;

            // Act
            product.SetCode(newCode);

            // Assert
            Assert.AreEqual(newCode.ToLowerInvariant(), product.Code);
            Assert.IsTrue(product.UpdatedAt >= beforeUpdate);
        }

        [Test]
        public void SetName_Throws_Exception_If_Name_Is_Empty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Initial Product";
            var code = "IP123";
            var price = 10.99m;
            var createdAt = DateTime.UtcNow;
            var product = new Product(id, name, code, price, createdAt);

            // Act & Assert
            Assert.Throws<Exception>(() => product.SetName(""));
            Assert.Throws<Exception>(() => product.SetName(" "));
            Assert.Throws<Exception>(() => product.SetName(null));
        }

        [Test]
        public void SetCode_Throws_Exception_If_Code_Is_Empty()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Initial Product";
            var code = "IP123";
            var price = 10.99m;
            var createdAt = DateTime.UtcNow;
            var product = new Product(id, name, code, price, createdAt);

            // Act & Assert
            Assert.Throws<Exception>(() => product.SetCode(""));
            Assert.Throws<Exception>(() => product.SetCode(" "));
            Assert.Throws<Exception>(() => product.SetCode(null));
        }
    }
}
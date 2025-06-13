using System;
using System.Collections.Generic;
using Backend.Controllers;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Backend.Tests.Controllers
{
    public class PhonesControllerTests
    {
        private readonly Mock<IPhoneService> _mockPhoneService;
        private readonly PhonesController _controller;

        public PhonesControllerTests()
        {
            _mockPhoneService = new Mock<IPhoneService>();
            _controller = new PhonesController(_mockPhoneService.Object);
        }

        [Fact]
        public void GetAllPhones_ReturnsOkResult_WithListOfPhones()
        {
            // Arrange
            var phones = new List<Phone>
            {
                new Phone { Id = 1, Name = "Phone 1", Manufacturer = "Manufacturer 1", PhoneNumber = "123456789", Price = 999.99m, InStock = true },
                new Phone { Id = 2, Name = "Phone 2", Manufacturer = "Manufacturer 2", PhoneNumber = "987654321", Price = 899.99m, InStock = false }
            };
            _mockPhoneService.Setup(service => service.GetAllPhones()).Returns(phones);

            // Act
            var result = _controller.GetAllPhones();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPhones = Assert.IsType<List<Phone>>(okResult.Value);
            Assert.Equal(2, returnedPhones.Count);
        }

        [Fact]
        public void GetPhone_WithValidId_ReturnsOkResult_WithPhone()
        {
            // Arrange
            var phone = new Phone { Id = 1, Name = "Phone 1", Manufacturer = "Manufacturer 1", PhoneNumber = "123456789", Price = 999.99m, InStock = true };
            _mockPhoneService.Setup(service => service.GetPhoneById(1)).Returns(phone);

            // Act
            var result = _controller.GetPhone(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedPhone = Assert.IsType<Phone>(okResult.Value);
            Assert.Equal(1, returnedPhone.Id);
        }

        [Fact]
        public void GetPhone_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockPhoneService.Setup(service => service.GetPhoneById(999)).Returns((Phone)null);

            // Act
            var result = _controller.GetPhone(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void CreatePhone_WithValidPhone_ReturnsCreatedAtAction()
        {
            // Arrange
            var phone = new Phone { Name = "New Phone", Manufacturer = "New Manufacturer", PhoneNumber = "123456789", Price = 999.99m, InStock = true };
            var createdPhone = new Phone { Id = 1, Name = "New Phone", Manufacturer = "New Manufacturer", PhoneNumber = "123456789", Price = 999.99m, InStock = true };
            _mockPhoneService.Setup(service => service.AddPhone(It.IsAny<Phone>())).Returns(createdPhone);

            // Act
            var result = _controller.CreatePhone(phone);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPhone = Assert.IsType<Phone>(createdAtActionResult.Value);
            Assert.Equal(1, returnedPhone.Id);
        }

        [Fact]
        public void UpdatePhone_WithValidIdAndPhone_ReturnsOkResult()
        {
            // Arrange
            var phone = new Phone { Id = 1, Name = "Updated Phone", Manufacturer = "Updated Manufacturer", PhoneNumber = "123456789", Price = 999.99m, InStock = true };
            _mockPhoneService.Setup(service => service.UpdatePhone(1, It.IsAny<Phone>())).Returns(phone);

            // Act
            var result = _controller.UpdatePhone(1, phone);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedPhone = Assert.IsType<Phone>(okResult.Value);
            Assert.Equal("Updated Phone", returnedPhone.Name);
        }

        [Fact]
        public void UpdatePhone_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var phone = new Phone { Id = 999, Name = "Updated Phone", Manufacturer = "Updated Manufacturer", PhoneNumber = "123456789", Price = 999.99m, InStock = true };
            _mockPhoneService.Setup(service => service.UpdatePhone(999, It.IsAny<Phone>())).Returns((Phone)null);

            // Act
            var result = _controller.UpdatePhone(999, phone);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeletePhone_WithValidId_ReturnsNoContent()
        {
            // Arrange
            _mockPhoneService.Setup(service => service.DeletePhone(1)).Returns(true);

            // Act
            var result = _controller.DeletePhone(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeletePhone_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockPhoneService.Setup(service => service.DeletePhone(999)).Returns(false);

            // Act
            var result = _controller.DeletePhone(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
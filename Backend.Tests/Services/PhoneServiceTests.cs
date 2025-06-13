using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Backend.Services;
using Xunit;

namespace Backend.Tests.Services
{
    public class PhoneServiceTests
    {
        private readonly PhoneService _phoneService;

        public PhoneServiceTests()
        {
            _phoneService = new PhoneService();
        }

        [Fact]
        public void GetAllPhones_ReturnsAllPhones()
        {
            // Act
            var result = _phoneService.GetAllPhones();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Phone>>(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void GetPhoneById_WithValidId_ReturnsPhone()
        {
            // Arrange
            int validId = 1;

            // Act
            var result = _phoneService.GetPhoneById(validId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(validId, result.Id);
        }

        [Fact]
        public void GetPhoneById_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var result = _phoneService.GetPhoneById(invalidId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddPhone_AddsPhoneToCollection()
        {
            // Arrange
            var newPhone = new Phone
            {
                Name = "Test Phone",
                Manufacturer = "Test Manufacturer",
                PhoneNumber = "123456789",
                Price = 999.99m,
                InStock = true
            };

            // Act
            var result = _phoneService.AddPhone(newPhone);
            var allPhones = _phoneService.GetAllPhones();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Contains(allPhones, p => p.Id == result.Id);
        }

        [Fact]
        public void UpdatePhone_WithValidId_UpdatesPhone()
        {
            // Arrange
            var phone = _phoneService.GetAllPhones().First();
            var updatedPhone = new Phone
            {
                Id = phone.Id,
                Name = "Updated Name",
                Manufacturer = phone.Manufacturer,
                PhoneNumber = phone.PhoneNumber,
                Price = phone.Price,
                InStock = phone.InStock
            };

            // Act
            var result = _phoneService.UpdatePhone(phone.Id, updatedPhone);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.Name);
        }

        [Fact]
        public void UpdatePhone_WithInvalidId_ReturnsNull()
        {
            // Arrange
            int invalidId = 999;
            var updatedPhone = new Phone
            {
                Id = invalidId,
                Name = "Updated Name",
                Manufacturer = "Test",
                PhoneNumber = "123456789",
                Price = 999.99m,
                InStock = true
            };

            // Act
            var result = _phoneService.UpdatePhone(invalidId, updatedPhone);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeletePhone_WithValidId_RemovesPhone()
        {
            // Arrange
            var phone = _phoneService.GetAllPhones().First();
            int phoneId = phone.Id;

            // Act
            var result = _phoneService.DeletePhone(phoneId);
            var phoneAfterDelete = _phoneService.GetPhoneById(phoneId);

            // Assert
            Assert.True(result);
            Assert.Null(phoneAfterDelete);
        }

        [Fact]
        public void DeletePhone_WithInvalidId_ReturnsFalse()
        {
            // Arrange
            int invalidId = 999;

            // Act
            var result = _phoneService.DeletePhone(invalidId);

            // Assert
            Assert.False(result);
        }
    }
}
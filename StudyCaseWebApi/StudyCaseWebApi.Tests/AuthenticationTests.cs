using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;
using StudyCaseWebApi.Services;
using StudyCaseWebApi.Services.Entities;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StudyCaseWebApi.Tests
{
    public class AuthenticationTests
    {
        private readonly Mock<IDataBaseService> _mockDbService;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IConfigurationSection> _mockConfigSection;

        public AuthenticationTests()
        {
            // Setup configuration mock
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfigSection = new Mock<IConfigurationSection>();
            
            _mockConfigSection.Setup(s => s.Value).Returns("test_secret_key_for_jwt_token_generation_testing");
            _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("test_secret_key_for_jwt_token_generation_testing");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("test_issuer");
            
            // Setup database service mock
            _mockDbService = new Mock<IDataBaseService>();
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsSuccessResponse()
        {
            // Arrange
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Password1!");
            var users = new List<Users>
            {
                new Users(Guid.NewGuid(), "testuser", "test@example.com", hashedPassword, false)
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Users>>();
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _mockDbService.Setup(db => db.Users()).Returns(mockDbSet.Object);

            var loginResponse = new LoginResponse("testuser", "test@example.com", "Password1!", false);

            // Act
            // Note: This test is simplified as we can't fully test the JWT token generation without refactoring the code
            // In a real scenario, we would refactor the code to make it more testable
            var result = await loginResponse.Login(_mockConfiguration.Object, _mockDbService.Object);

            // Assert
            Assert.True(result.ResponseResult);
            Assert.Contains("successfully logged in", result.ResponseMessage);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsFailureResponse()
        {
            // Arrange
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("Password1!");
            var users = new List<Users>
            {
                new Users(Guid.NewGuid(), "testuser", "test@example.com", hashedPassword, false)
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Users>>();
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.Provider).Returns(users.Provider);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.Expression).Returns(users.Expression);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.ElementType).Returns(users.ElementType);
            mockDbSet.As<IQueryable<Users>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());

            _mockDbService.Setup(db => db.Users()).Returns(mockDbSet.Object);

            var loginResponse = new LoginResponse("testuser", "test@example.com", "WrongPassword1!", false);

            // Act
            var result = await loginResponse.Login(_mockConfiguration.Object, _mockDbService.Object);

            // Assert
            Assert.False(result.ResponseResult);
            Assert.Equal("Incorrect password", result.ResponseMessage);
        }
    }
}
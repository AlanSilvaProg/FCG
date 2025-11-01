using System;
using Xunit;
using StudyCaseWebApi.Extensions;
using StudyCaseWebApi.Models;

namespace StudyCaseWebApi.Tests
{
    public class LoginResponseExtensionTests
    {
        [Fact]
        public void IsValidEmail_WithValidEmail_ReturnsTrue()
        {
            // Arrange
            var loginResponse = new LoginResponse("testuser", "test@example.com", "Password123!", false);
            
            // Act
            bool result = loginResponse.IsValidEmail();
            
            // Assert
            Assert.True(result);
        }
        
        [Theory]
        [InlineData("invalid")]
        [InlineData("invalid@")]
        [InlineData("@example.com")]
        [InlineData("")]
        public void IsValidEmail_WithInvalidEmail_ReturnsFalse(string email)
        {
            // Arrange
            var loginResponse = new LoginResponse("testuser", email, "Password123!", false);
            
            // Act
            bool result = loginResponse.IsValidEmail();
            
            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void IsValidEmail_WithNullEmail_ReturnsFalse()
        {
            // Arrange
            var loginResponse = new LoginResponse("testuser", null, "Password123!", false);
            
            // Act
            bool result = loginResponse.IsValidEmail();
            
            // Assert
            Assert.False(result);
        }
        
        [Theory]
        [InlineData("Password1!", true)]  // Valid: uppercase, digit, special char, 8+ chars
        [InlineData("Abcdef1!", true)]    // Valid: uppercase, digit, special char, 8+ chars
        [InlineData("password1!", false)] // Invalid: no uppercase
        [InlineData("Password!", false)]  // Invalid: no digit
        [InlineData("PASSWORD1", false)]  // Invalid: no special char
        [InlineData("Pass1!", false)]     // Invalid: too short
        [InlineData("", false)]           // Invalid: empty
        public void IsValidPassword_ValidatesCorrectly(string password, bool expected)
        {
            // Arrange
            var loginResponse = new LoginResponse("testuser", "test@example.com", password, false);
            
            // Act
            bool result = loginResponse.IsValidPassword();
            
            // Assert
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void IsValidPassword_WithNullPassword_ReturnsFalse()
        {
            // Arrange
            var loginResponse = new LoginResponse("testuser", "test@example.com", null, false);
            
            // Act
            bool result = loginResponse.IsValidPassword();
            
            // Assert
            Assert.False(result);
        }
    }
}
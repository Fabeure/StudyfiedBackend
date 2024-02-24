/*using System;
using System.Net;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyfiedBackend.Controllers;
using StudyfiedBackend.Dtos;
using StudyfiedBackend.Models;
using Xunit;

namespace StudyfiedBackendUnitTests.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly UserManager<ApplicationUser> _fakeUserManager;

        public AuthenticationControllerTests()
        {
            // Arrange
            _fakeUserManager = A.Fake<UserManager<ApplicationUser>>();
        }

        [Fact]
        public async Task Register_ValidRequest_ShouldReturnOk()
        {
            // Arrange
            var controller = new AuthenticationController(_fakeUserManager);
            var registerRequest = new RegisterRequest
            {
                FullName = "John Doe",
                Email = "example@example.com",
                Password = "test123TEST&"
            };

            // Act
            var result = await controller.Register(registerRequest) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var registerResponse = result.Value.Should().BeAssignableTo<RegisterResponse>().Subject;
            registerResponse.Success.Should().BeTrue();
            registerResponse.Message.Should().Be("User registered successfully");
        }

        [Fact]
        public async Task Register_UserAlreadyExists_ShouldReturnBadRequest()
        {
            // Arrange
            A.CallTo(() => _fakeUserManager.FindByEmailAsync(A<string>._)).Returns(new ApplicationUser());
            var controller = new AuthenticationController(_fakeUserManager);
            var registerRequest = new RegisterRequest
            {
                FullName = "John Doe",
                Email = "example@example.com",
                Password = "password123"
            };

            // Act
            var result = await controller.Register(registerRequest) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            var errorMessage = result.Value.Should().BeAssignableTo<string>().Subject;
            errorMessage.Should().Be("User already exists");
        }

        // Add more test cases for the Register method with different scenarios

        [Fact]
        public async Task Login_ValidCredentials_ShouldReturnOk()
        {
            // Arrange
            A.CallTo(() => _fakeUserManager.FindByEmailAsync(A<string>._)).Returns(new ApplicationUser());
            A.CallTo(() => _fakeUserManager.GetRolesAsync(A<ApplicationUser>._)).Returns(new List<string>());
            var controller = new AuthenticationController(_fakeUserManager);
            var loginRequest = new LoginRequest
            {
                Email = "john.doe@example.com",
                Password = "password123"
            };

            // Act
            var result = await controller.Login(loginRequest) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var loginResponse = result.Value.Should().BeAssignableTo<LoginResponse>().Subject;
            loginResponse.Success.Should().BeTrue();
            loginResponse.Message.Should().Be("Login Successful");
            loginResponse.AccessToken.Should().NotBeNullOrEmpty();
            loginResponse.Email.Should().Be("john.doe@example.com");
            loginResponse.UserId.Should().NotBeNullOrEmpty();
        }

        // Add more test cases for the Login method with different scenarios

        // Add tests for other methods in the AuthenticationController

    }
}*/
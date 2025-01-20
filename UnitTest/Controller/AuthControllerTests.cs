using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controller;

namespace UnitTest.Controller
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController authController;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Authentication_ReturnOkResult_WhenDataExists()
        {
            //arrange
            var loginData = new LoginDTO { Email = "test@gmail.com", Password = "Test1234" };
            _authServiceMock.Setup(x => x.SignIn(It.IsAny<Employee>())).ReturnsAsync(new ServiceResponse(true, "Login Successfully"));

            //act
            var result = await authController.Login(loginData);

            // Assets
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnValue = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnValue);
        }

        [Fact]
        public async Task Authentication_ReturnIncorrectEmail_Password()
        {
            //arrange
            var loginData = new LoginDTO { Email = "test@gmail.com", Password = "asldfjsal" };
            _authServiceMock.Setup(x => x.SignIn(It.IsAny<Employee>())).ReturnsAsync(new ServiceResponse(false, "Invalid Email or Password"));

            //act
            var result = await authController.Login(loginData);

            // Assert
            var OkResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var ReturnResult = Assert.IsType<UnauthorizedObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task AddNewEmployee_ReturnsOkResult_WhenEmployeeAdded()
        {
            //arrang
            var newEmployee = new Employee {Id=1, Full_Name="Test" , Email="test@gmail.com",Password="Test1234",Designation="Test"};
            _authServiceMock.Setup(x => x.SignUp(It.IsAny<Employee>())).ReturnsAsync(new ServiceResponse(true,"Employee Added Successfully"));

            //act
            var result = await authController.SignUp(newEmployee);

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnValue = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnValue);
        }


    }
}

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
            if (result == null)
            {
                var notnull = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notnull);
            }
            else { 
                var OkResult = Assert.IsType<OkObjectResult>(result);
                var ReturnValue = Assert.IsType<OkObjectResult>(OkResult);
                Assert.NotNull(ReturnValue);
            }
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
            if (result== null)
            {
                var notnull = Assert.IsType<NotFoundResult>(result);
                Assert.NotNull(notnull);
            }
            else
            {
                var OkResult = Assert.IsType<OkObjectResult>(result);
                var ReturnValue = Assert.IsType<OkObjectResult>(OkResult);
                Assert.NotNull(ReturnValue);
            }
        }


    }
}

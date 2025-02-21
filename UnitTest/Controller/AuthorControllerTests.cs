using BusinessLayer.CustomException;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controller;

namespace UnitTest.Controller
{
    public  class AuthorControllerTests
    {
        private readonly Mock<IAuthorService> _authorServiceMock;
        private readonly AuthorController authorController;

        public AuthorControllerTests()
        {
            _authorServiceMock = new Mock<IAuthorService>();
            authorController = new AuthorController(_authorServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAuthor_OkReturnOk_WhenDataAvailable()
        {
            //arrange
            _authorServiceMock.Setup(x => x.GetAllAuthorData()).ReturnsAsync(new List<Author> 
                { 
                    new Author
                    {
                        Id = 1,
                        Name = "Test",
                        Biography = "Test"
                    }
                });
            //act
            var result = await authorController.GetAllAuthorData();

            //asset
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task GetAllAuthor_OkReturnOk_WhenDataNotAvailable()
        {
            //arrange
            _authorServiceMock.Setup(x => x.GetAllAuthorData()).ReturnsAsync(new List<Author> { });

            //act
            var result = await authorController.GetAllAuthorData();

            //Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnResult = Assert.IsType<NotFoundObjectResult>(okResult);
            Assert.NotNull(returnResult);
        }

        [Fact]
        public async Task GetAuthorById_OkReturnOk_WhenDataAvailable()
        {
            //arrange
            int id = 1;
            _authorServiceMock.Setup(x => x.GetAuthorById(id)).ReturnsAsync(new Author
            {
                Id = id,
                Name = "Test",
                Biography = "Test"
            });

            //act 
            var result = await authorController.GetAuthorDataById(id);

            //Assert
            var okResult =Assert.IsType<OkObjectResult>(result);
            var returnResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(returnResult);
        }

        [Fact]
        public async Task GetAuthorById_BadRequest()
        {
            //arrange
            int id = 0;
            _authorServiceMock.Setup(x => x.GetAuthorById(id)).ThrowsAsync(new DataCustomException($"Please Pass the Valid Id {id}"));

            //act
            var result = await authorController.GetAuthorDataById(id);

            //Assert
            var okresult = Assert.IsType<BadRequestObjectResult>(result);
            var returnresult = Assert.IsType<BadRequestObjectResult>(okresult);
            Assert.NotNull(returnresult);
        }

        [Fact]
        public async Task GetAuthorById_DataNotFound()
            {
            //arrange
            int id = 1;
            _authorServiceMock.Setup(x => x.GetAuthorById(id)).ReturnsAsync((Author)null);

            //act
            var result = await authorController.GetAuthorDataById(id);

            //Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnResult = Assert.IsType<NotFoundObjectResult> (okResult);
            Assert.NotNull(returnResult);
        }

        [Fact]
        public async Task AddAuthorDetail()
        {
            //arrange
            var data = new AuthorDTO
            {
                Name = "Test",
                Biography = "Test"
            };
            _authorServiceMock.Setup(x => x.AddAuthorData(It.IsAny<Author>())).ReturnsAsync(new ServiceResponse(true,"Author Added Successfully"));

            //act
            var result = await authorController.AddAuthorDetail(data);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnResult = Assert.IsType<OkObjectResult> (okResult);
            Assert.NotNull(returnResult);
        }

        [Fact]
        public async Task AddAuthorDetail_WhenSomethingError()
        {
            //arrange
            var data = new AuthorDTO {
                Biography="Test",
            };
            _authorServiceMock.Setup(x => x.AddAuthorData(It.IsAny<Author>())).ReturnsAsync();
            //act
            var result = await authorController.AddAuthorDetail(data);

            //assert

        }
    }
}

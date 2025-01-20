using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controller;


namespace UnitTest.Controller
{
    public class BookControllerTests
    {
        private readonly Mock<IBookService> _bookServiceMock;
        private readonly BookController _controller;

        public BookControllerTests()
        {
            _bookServiceMock = new Mock<IBookService>();
            _controller = new BookController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task AddBookData_OkResultOk_WhenBookAdded()
        {
            //arrang
            var newBook = new BookDTO {Title="Test",Author="Test", Genre="Test", PublicationYear =2000};
            _bookServiceMock.Setup(x => x.AddBookData(It.IsAny<Book>())).ReturnsAsync(new ServiceResponse(true,"Book data Added Successfully"));

            //act
            var result = await _controller.AddBook(newBook);

            //Assert
            if (result== null)
            {
                var notnull = Assert.IsType<NotFoundObjectResult>(result);
               Assert.NotNull(notnull);
            }
            else
            {
                var OkResult = Assert.IsType<OkObjectResult>(result);
                var ReturnResult = Assert.IsType<OkObjectResult>(OkResult);
                Assert.NotNull(ReturnResult);
            }
        }

        [Fact]
        public async Task UpdateBookData_OkResultOk_WhenBookUpdated()
        {
            //arrange
            var Id = 1;
            var updateBook = new BookDTO { Title = "Test", Author = "Test", Genre = "Test", PublicationYear = 2000 };
            _bookServiceMock.Setup(x => x.UpdateBookData(Id, It.IsAny<Book>())).ReturnsAsync(new ServiceResponse(true, "Book Data Updated Successfully"));

            //act 
            var result = await _controller.UpdateBookDetails(Id, updateBook);

            //Assert
            if (result== null)
            {
                var notnull = Assert.IsType<NotFoundObjectResult>(result);
                Assert.NotNull(notnull);
            }
            else
            {
                var OkResult= Assert.IsType<OkObjectResult>(result);
                var ReturnResult = Assert.IsType<OkObjectResult>(OkResult);
                Assert.NotNull(ReturnResult);
            }
        }
    }
}

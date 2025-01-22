using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
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
        public async Task GetAllBook_OkReturnOk_WhenDataAvailable()
        {
            // arrange
            _bookServiceMock.Setup(x => x.GetAllBookData()).ReturnsAsync(new List<BookData>
                {
                    new BookData
                    {
                        Id= 1,
                        Title="Test",
                        Author="Test",
                        Genre="Test",
                        PublicationYear=2000
                    }
                });

            //act
            var result = await _controller.GetAllBooks();

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult= Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }
        // when data is empty
        [Fact]
        public async Task GetAllBooks_ReturnNotNull_WhenDataIsNotAvailable()
        {
            //arrange
            _bookServiceMock.Setup(x => x.GetAllBookData()).ReturnsAsync(new List<BookData>{});

            //act
            var result = await _controller.GetAllBooks();

            //Assert
            var OkResult =Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        // getdatabyid when data is available
        [Fact]
        public async Task GetBookById_Return_OKObject_WhenDataAvailable()
        {
            //arrange
            var Id = 1;
            _bookServiceMock.Setup(x => x.GetBookById(Id)).ReturnsAsync(new BookData
            {
                Id = 1,
                Title = "Test",
                Author = "Test",
                Genre = "Test",
                PublicationYear = 2000
            });

            //act
            var result = await _controller.GetBookById(Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task GetBookById_Return_NotFound_WhenDataNotExits()
        {
            //arrange
            var Id = 1;
            _bookServiceMock.Setup(x => x.GetBookById(Id)).ReturnsAsync((BookData)null);

            //act
            var result = await _controller.GetBookById(Id);

            //Assert
            var OkResult = Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task GetBookById_Return_BadRequest()
        {
            //arrange
            var Id = 0;
            //act
            var result = await _controller.GetBookById(Id);
            //Assert
            var OkResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnResult = Assert.IsType<BadRequestObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }
        // add data
        [Fact]
        public async Task AddBookData_OkResultOk_WhenBookAdded()
        {
            //arrang
            var newBook = new BookDTO {Title="Test",Author="Test", Genre="Test", PublicationYear =2000};
            _bookServiceMock.Setup(x => x.AddBookData(It.IsAny<Book>())).ReturnsAsync(new ServiceResponse(true,"Book data Added Successfully"));

            //act
            var result = await _controller.AddBook(newBook);

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        // update data
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

        //delete testcase
        [Fact]
        public async Task DeleteBookData_WhenDataAvailable()
        {
            //arrange
            var Id = 1;
            _bookServiceMock.Setup(x => x.DeleteBookData(Id)).ReturnsAsync(new ServiceResponse (true,"Data deleted Successfully"));

            //act
            var result = await _controller.DeleteBookDetail(Id);

            //Assert
            var Okresult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(Okresult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task DeleteBookData_WhenDataIsNotAvailable()
        {
            //arrange
            var Id = 1;
            _bookServiceMock.Setup(x => x.DeleteBookData(Id)).ReturnsAsync(new ServiceResponse(false,"Data is not found with this Id"));
            //act
            var result = await _controller.DeleteBookDetail(Id);
            //Assert
            var OkResult = Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);   
        }
        [Fact]
        public async Task DeleteBookData_WhenIdIsNotValid()
        {
            //arrange
            var Id = 0;
            //Act
            var result = await _controller.DeleteBookDetail(Id);
            //Assert
            var OkResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnResult =Assert.IsType<BadRequestObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }
    }
}

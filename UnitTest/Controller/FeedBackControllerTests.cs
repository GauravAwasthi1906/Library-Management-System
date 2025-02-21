using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controller;

namespace UnitTest.Controller
{
    public class FeedBackControllerTests
    {
        private readonly Mock<IFeedBackService> _feedbackServiceMock;
        private readonly FeedBackController _controller;

        public FeedBackControllerTests()
        {

            _feedbackServiceMock = new Mock<IFeedBackService>();
            _controller = new FeedBackController(_feedbackServiceMock.Object);
        }

        [Fact]
        public async Task GetAllFeedBack_OkReturnOk_WhenDataAvailable()
        {
            //arrange
            _feedbackServiceMock.Setup(x => x.GetAllFeedBacks()).ReturnsAsync(new List<FeedbackData>
            {
                new FeedbackData
                {
                    Id = 1,
                    MemberId=1,
                    MemberName="Test",
                    MemberContactInfo="Test",
                    MembershipDate = DateTime.Now,
                    DateSubmitted=DateTime.Now,
                    Comment= "Test",
                }
            });
            //act 
            var result = await _controller.GetAllFeedback();

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }
        [Fact]
        public async Task GetAllFeedBack_OkNotFound_WhenDataUnavailable()
        {
            //arrange
            _feedbackServiceMock.Setup(x => x.GetAllFeedBacks()).ReturnsAsync(new List<FeedbackData> { });

            //act
            var result = await _controller.GetAllFeedback();

            //Assert
            var OkResult= Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }


        [Fact]
        public async Task GetAllFeedBack_OkReturnOK_WhenDataAvailable()
        {
            //arrange
            int id = 1;
            _feedbackServiceMock.Setup(x => x.GetFeedBack(id)).ReturnsAsync(new FeedbackData
            {
                Id = 1,
                MemberId = 1,
                MemberName = "Test",
                MemberContactInfo = "Test",
                MembershipDate = DateTime.Now,
                DateSubmitted = DateTime.Now,
                Comment = "Test",
            });

            //act
            var result = await _controller.GetFeedBackById(id);

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }
        [Fact]
        public async Task GetAllFeedBack_OkNotFound_WhenDataIsNotAvailable()
        {
            //arrange
            int id = 1;
            _feedbackServiceMock.Setup(x => x.GetFeedBack(id)).ReturnsAsync((FeedbackData)null);

            //act
            var result = await _controller.GetFeedBackById(id);

            //Assert
            var okResult = Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(okResult);
            Assert.NotNull(ReturnResult);
        }
        [Fact]
        public async Task GetAllFeedBack_OkBadRequest()
        {
            //arrange
            int id = 0;
            _feedbackServiceMock.Setup(x => x.GetFeedBack(id)).ReturnsAsync((FeedbackData)null);

            //act
            var result = await _controller.GetFeedBackById(id);

            //Assert
            var OkResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnResult = Assert.IsType<BadRequestObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);

        }

        [Fact]
        public async Task AddnewFeedBack_Return_OKResult()
        {
            //arrange
            var newFeedBack = new FeedbackDTO {  MemberId=1, Comment="Test", DateSubmitted=DateTime.Now };
            _feedbackServiceMock.Setup(x=>x.AddFeedBack(It.IsAny<Feedback>())).ReturnsAsync(new ServiceResponse(true,"FeedBack added successfully"));

            //act
            var result = await _controller.AddFeedBack(newFeedBack);

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult =Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task AddnewFeedBack_ReturnBadRequest()
        {
            //arrange
            var newFeedBack = new FeedbackDTO {Comment = "Test", DateSubmitted = DateTime.Now };
            _feedbackServiceMock.Setup(x=>x.AddFeedBack(It.IsAny<Feedback>())).ReturnsAsync(new ServiceResponse(false,"Please Filled all the Entries"));

            //act
            var result = await _controller.AddFeedBack(newFeedBack);

            //assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult= Assert.IsType<OkObjectResult>(OkResult);  
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task UpdateFeedBack_ReturnOkReturn()
        {
            //arrange
            int Id = 1;
            var updatedata = new FeedbackDTO {MemberId = 1, Comment = "Test", DateSubmitted = DateTime.Now };
            _feedbackServiceMock.Setup(x => x.UpdateFeedBack(Id, It.IsAny<Feedback>())).ReturnsAsync(new ServiceResponse(true,"Feedback updated successfully"));

            //act
            var result = await _controller.UpdateFeedBack(Id,updatedata);

            //Assert
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var RetrunResult = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(RetrunResult);
        }
        [Fact]
        public async Task UpdateFeedBack_ReturnBadRequest()
        {
            //arrange
            int Id = 1;
            var updatedata = new FeedbackDTO {Comment = "Test", DateSubmitted = DateTime.Now };
            _feedbackServiceMock.Setup(x=>x.UpdateFeedBack(Id,It.IsAny<Feedback>()));

            //act
            _controller.ModelState.AddModelError("MemberId","This is required");
            _controller.ModelState.AddModelError("Comment", "This is required");
            _controller.ModelState.AddModelError("DateSubmitted", "This is required");
            var result = await _controller.UpdateFeedBack(Id, updatedata);

            //Assert
            var OkResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnResult = Assert.IsType<BadRequestObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
            
        }

        // delete test case
        [Fact]
        public async Task DeleteFeedBackData_WhenDataAvailable()
        {
            //arrange
            int id = 1;
            _feedbackServiceMock.Setup(x => x.DeleteFeedBack(id)).ReturnsAsync(new ServiceResponse(true,"Feedback Deleted Successfully"));

            //act
            var result = await _controller.DeleteFeedBack(id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ReturnResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task DeleteFeedBackData_WhenDataIsNotAvailable()
        {   
            //arrange
            int id = 1;
            _feedbackServiceMock.Setup(x => x.DeleteFeedBack(id)).ReturnsAsync(new ServiceResponse(false,"Data not found "));

            //act 
            var result = await _controller.DeleteFeedBack(id);

            //Assert
            var OkResult = Assert.IsType<NotFoundObjectResult>(result);
            var ReturnResult = Assert.IsType<NotFoundObjectResult>(OkResult);
            Assert.NotNull(ReturnResult);
        }

        [Fact]
        public async Task DeleteFeedBackData_ReturnBadRequest()
        {
            //arrange
            int id = 0;
            _feedbackServiceMock.Setup(x => x.DeleteFeedBack(id)).ReturnsAsync(new ServiceResponse(false,"Data can not be deleted"));

            //act
            var result = await _controller.DeleteFeedBack(id);

            //Assert
            var okResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnResult = Assert.IsType<BadRequestObjectResult>(okResult);
            Assert.NotNull(ReturnResult);
        }

    }
}

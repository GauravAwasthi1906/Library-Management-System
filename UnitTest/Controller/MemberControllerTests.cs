using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.DataDTOs;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controller;


namespace UnitTest.Controller
{
    public class MemberControllerTests
    {
        private readonly Mock<IMemberService> _memberService;
        private readonly MemberController _controller;

        public MemberControllerTests() { 
            _memberService = new Mock<IMemberService>();
            _controller = new MemberController(_memberService.Object);
        }


        [Fact]
        public async Task GetAllMembers_ReturnsOkResult_WhenDataExists()
        {
            // arrange
            _memberService.Setup(x => x.GetAllMembers()).ReturnsAsync(new List<MemberData>
            {   
                new MemberData
                {
                    Id = 1,
                    Name = "Test",
                    ContactInfo = "Test",
                    MembershipDate = DateTime.Now
                }
            });

            // act as
            var data = await _controller.GetAllMembers();

            //assert
            var okResult = Assert.IsType<OkObjectResult>(data);
            var returnValue = Assert.IsType<OkObjectResult>(okResult);
            Assert.NotNull(returnValue);
            
        }

        [Fact]
        public async Task GetMemberById_ReturnsOkResult_WhenMemberExists()
        {
            // arrang
            var Id = 1;
            _memberService.Setup(x => x.GetMemberById(Id)).ReturnsAsync(
                new MemberData
                {
                    Id = 1,
                    Name = "Test",
                    ContactInfo = "Test",
                    MembershipDate = DateTime.Now
                }
            );

            //act as 
            var data = await _controller.GetMemberById( Id );

            //Assets
            var OkResult = Assert.IsType<OkObjectResult>(data);
            var ReturnValue= Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnValue);
            
        }

        [Fact]
        public async Task AddNewMember_ReturnsOkResult_WhenMemberAdded()
        {
            //arrang
            var newMember = new MemberDTO { Id = 1, Name = "Test", ContactInfo = "Test", MembershipDate = DateTime.Now };
            _memberService.Setup(x => x.AddMember(It.IsAny<Member>())).ReturnsAsync(new ServiceResponse(true,"Member Added Successfully"));

            //act 
            var result = await _controller.AddNewMember(newMember);

            //Assets   
            var OkResult = Assert.IsType<OkObjectResult>(result);
            var ReturnValue = Assert.IsType<OkObjectResult>(OkResult);
            Assert.NotNull(ReturnValue);
        }

        [Fact]
        public async Task UpdateMember_ReturnsOkResult_WhenMemberUpdated()
        {
            //arrang
            var Id = 1;
            var updateMember = new MemberDTO {Name = "Test", ContactInfo = "Test", MembershipDate = DateTime.Now };

            _memberService.Setup(x => x.UpdateMember(Id,It.IsAny<Member>())).ReturnsAsync(new ServiceResponse(true, "Member Updated Successfully"));

            // act
            var result =await _controller.UpdateMember(Id, updateMember);

            //Assert
            var Okresult = Assert.IsType<OkObjectResult>(result);
            var ReturnValue = Assert.IsType<OkObjectResult>(Okresult);
            Assert.NotNull(ReturnValue);
        }

        [Fact]
        public async Task DeleteMember_ReturnsOkResult_WhenMemberDeleted()
        {
            //Arrang
            var Id = 1;
            _memberService.Setup(x => x.DeleteMember(Id)).ReturnsAsync(new ServiceResponse(true, "Member Deleted Successfully"));

            //act
            var result = await _controller.DeleteMember(Id);

            //Assert
            var Okresult = Assert.IsType<OkObjectResult>(result);
            var ReturnValue = Assert.IsType<OkObjectResult>(Okresult);
            Assert.NotNull(ReturnValue);
        }

        [Fact]
        public async Task DeleteMember_ReturnBadResult_WhenMemerDeleted()
        {
            //arrange
            var Id = 0;
            _memberService.Setup(x => x.DeleteMember(Id)).ReturnsAsync(new ServiceResponse(false,"Please Enter the Valid Id"));

            // act 
            var result = await _controller.DeleteMember(Id);

            //Assert
            var OkResult = Assert.IsType<BadRequestObjectResult>(result);
            var ReturnValue = Assert.IsType<BadRequestObjectResult>(OkResult);
            Assert.NotNull(ReturnValue);
        }

    }
}

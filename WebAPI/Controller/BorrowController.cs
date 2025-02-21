using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowService _borrowService;
        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }
        [HttpPost]
        public async Task<ActionResult> AddBorow(BorrowDTO borrowDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Borrow
                {
                    MemberId = borrowDTO.MemberId,
                    BookId = borrowDTO.BookId,
                    BorrowDate = borrowDTO.BorrowDate,
                    DueDate = borrowDTO.DueDate,
                };
                var data = await _borrowService.AddBorrow(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBorrow()
        {
            try {
                var data = await _borrowService.GetAllBorrows();
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBorrowById(int? id)
        {
            try {
                var data= await _borrowService.GetBorrowById(id);
                if (data == null)
                {
                    return NotFound("User not found");
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBorrow(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest("Please Enter the Valid Id");
                }
                var data = await _borrowService.DeleteBorrow(id);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBorrow([FromRoute] int? id, [FromBody] BorrowDTO borrowDTO)
        {
            try
            {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest("Please Enter the valid number");
                }
                var entity = new Borrow
                {
                    MemberId = borrowDTO.MemberId,
                    BookId = borrowDTO.BookId,
                    BorrowDate = borrowDTO.BorrowDate,
                    DueDate = borrowDTO.DueDate,
                };
                var data = await _borrowService.UpdateBorrow(id, entity);
                return Ok(data);
            }
            catch (Exception ex) { 
                return StatusCode(500, ex.Message); 
            }
        }
       
    }
}

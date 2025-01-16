using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllBooks()
        {
            try
            {
                var data = await _bookService.GetAllBookData();
                if (data.Any())
                {
                    return NotFound("Data not found");
                }
                return Ok(data);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBookById([FromRoute]int id)
        {
            try
            {
                var data = await _bookService.GetBookById(id);
                if (data== null)
                {
                    return NotFound("Data not found with this id");
                }
                return Ok(data);
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

       
    }
}

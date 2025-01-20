using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataAccessLayer.Entities;
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
                if (!data.Any())
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
        public async Task<ActionResult> GetBookById([FromRoute]int? id)
        {
            try
            {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest($"You are passing a Invalid Id {id}");
                }
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

        [HttpPost]
        public async Task<ActionResult> AddBook(BookDTO book)
        {
            try {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Book
                {
                    Title=book.Title,
                    Author=book.Author,
                    Genre= book.Genre,
                    PublicationYear = book.PublicationYear
                };
                var data = await _bookService.AddBookData(entity);
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBookDetail(int? id)
        {
            try {
                if (!id.HasValue || id<=0)
                {
                    return BadRequest($"Please Enter the valid ID {id}");
                }
                var data = await _bookService.DeleteBookData(id);
                if (!data.Flag)
                {
                    return NotFound($"data not found with this Id {id}");
                }
                return Ok(data);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookDetails(int id, BookDTO book)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = new Book
                {
                    Title = book.Title,
                    Author = book.Author,
                    Genre = book.Genre,
                    PublicationYear = book.PublicationYear
                };
                var data = await _bookService.UpdateBookData(id,entity);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
       
    }
}

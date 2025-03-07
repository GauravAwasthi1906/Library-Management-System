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
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllAuthorData()
        {
            try
            {
                var data = await _authorService.GetAllAuthorData();
                if (!data.Any())
                {
                    return NotFound(new { message = "Data is Empty" });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAuthorDataById(int? id)
        {
            try
            {
                if (!id.HasValue || id <= 0)
                {
                    return BadRequest($"Please Pass the Valid Id {id}");
                }
                var data = await _authorService.GetAuthorById(id);
                if (data == null)
                {
                    return NotFound($"Data not found with this Id {id}");
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor([FromRoute] int? id)
        {
            try
            {
                if (!id.HasValue || id <= 0)
                {
                    return BadRequest(new { message = $"Please provide the valid Id {id}" });
                }
                var data = await _authorService.DeleteAuthorData(id);
                if (!data.Flag)
                {
                    return NotFound(new { message = $"The Data is not found with this Id {id}" });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddAuthorDetail([FromBody] AuthorDTO author)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _authorService.AddAuthorData(author);
                if (!data.Flag)
                {
                    return BadRequest(new { message = $"{data.Message}" });
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorDetail([FromRoute]int? id,[FromBody] AuthorDTO author)
         {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _authorService.UpdateAuthorData(id,author);
                if (!data.Flag)
                {
                    return BadRequest(new {message = "Something went wrong"});
                }
                return Ok(data);
            }
            catch (Exception ex) { 
                return StatusCode(500,ex.Message);  
            }
        }
    }
}
